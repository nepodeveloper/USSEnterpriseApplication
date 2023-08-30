using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USSEnterprise.Application.Interfaces;
using USSEnterprise.Domain.Entities;
using USSEnterprise.Domain.ValueObjects;

namespace USSEnterprise.Application.Services
{
    public class ElevatorService : IElevatorService
    {
        
        private readonly bool [] floorRequests;
        public int CurrentFloor { get; private set; } = 0;
        private readonly int topFloor;
        public ElevatorStatus Status { get; private set; } = ElevatorStatus.STOPPED;

        public ElevatorService(Elevator elevator, int numberOfFloors = 10)
        {
            floorRequests = new bool[numberOfFloors + 1];
            topFloor = numberOfFloors;
        }

        public void RequestFloor(int floor)
        {
            if (floor < 1 || floor > topFloor)
            {
                Console.WriteLine($"Invalid floor request. Please select a floor between 1 and {topFloor}.");
                return;
            }
            floorRequests[floor] = true;
            ProcessRequests();
        }
        private void Stop(int floor)
        {
            Status = ElevatorStatus.STOPPED;
            CurrentFloor = floor;
            floorRequests[floor] = false;
            Console.WriteLine($"Stopped at floor {floor}");
        }

        private void ProcessRequests()
        {
            if (Status == ElevatorStatus.STOPPED)
            {
                int nextFloor = FindNextRequestedFloor();

                if (nextFloor != -1)
                {
                    MoveToFloor(nextFloor);
                }
                else
                {
                    Console.WriteLine("Elevator is idle. Waiting for requests...");
                }
            }
            else if (Status == ElevatorStatus.MOVING)
            {
               //ADD Code
            }
            else if (Status == ElevatorStatus.SERVICE)
            {
                //ADD Code
            }
            else if (Status == ElevatorStatus.JAMMED)
            {
                //ADD Code
            }
            else if (Status == ElevatorStatus.OVERWEIGHT)
            {
                //ADD Code
            }
        }

        private int FindNextRequestedFloor()
        {
            for (int i = CurrentFloor; i <= topFloor; i++)
            {
                if (floorRequests[i])
                {
                    return i;
                }
            }

            for (int i = CurrentFloor; i >= 1; i--)
            {
                if (floorRequests[i])
                {
                    return i;
                }
            }

            return -1;
        }

        private void MoveToFloor(int targetFloor)
        {
            Status = CurrentFloor < targetFloor ? ElevatorStatus.UP : ElevatorStatus.DOWN;
    
            Console.WriteLine($"Closing Door...");

            while (CurrentFloor != targetFloor)
            {
                if (Status == ElevatorStatus.UP)
                {
                    CurrentFloor++;
                    Console.WriteLine($"Moving UP... Currently at floor {CurrentFloor}");
                }
                else if (Status == ElevatorStatus.DOWN)
                {
                    CurrentFloor--;
                    Console.WriteLine($"Moving Down... Currently at floor {CurrentFloor}");
                }
            }

            Stop(CurrentFloor);
        }
    }
}
