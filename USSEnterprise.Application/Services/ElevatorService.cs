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
        
        private bool [] floorRequests { get; set; }
        public int currentFloor { get; private set; } = 0;
        public int topFloor { get; private set; }
        public int currentWeight { get; private set; } = 0;
        public int averagePersonWeight { get; private set; } = 80;
        public int maxWeightCapacity { get; private set; } = 1400;
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
                Console.Write($"Invalid floor request. Please select a floor between 1 and {topFloor}.");
                return;
            }
            floorRequests[floor] = true;
            ProcessRequests();
        }
        private void Stop(int floor)
        {
            Status = ElevatorStatus.STOPPED;
            currentFloor = floor;
            floorRequests[floor] = false;
            Console.WriteLine($"Stopped at floor {floor}");
        }

        private void ProcessRequests()
        {
            currentWeight = EnterPassengers();
            if (currentWeight > maxWeightCapacity)
            {
                Console.WriteLine("Elevator is overweight. Cannot move.");
                Status = ElevatorStatus.OVERWEIGHT;
            }

            if (Status == ElevatorStatus.STOPPED)
            {
                int nextFloor = FindNextRequestedFloor();

                if (nextFloor != -1)
                {
                    MoveToFloor(nextFloor);
                }
                else
                {
                    Console.Write("Elevator is idle. Waiting for requests...");
                    Status = ElevatorStatus.IDLE;
                }
            }
            else if (Status == ElevatorStatus.OVERWEIGHT)
            {
                Console.Write("Elevator is overweight. Cannot move.");
                Status = ElevatorStatus.STOPPED;
            }
            else if (Status == ElevatorStatus.IDLE)
            {
                Console.Write("Elevator is idle, Please enter desired floor...");
                Status = ElevatorStatus.STOPPED;
            }
        }

        private int FindNextRequestedFloor()
        {
            for (int i = currentFloor; i <= topFloor; i++)
            {
                if (floorRequests[i])
                {
                    return i;
                }
            }

            for (int i = currentFloor; i >= 1; i--)
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
            Status = currentFloor < targetFloor ? ElevatorStatus.UP : ElevatorStatus.DOWN;
    
            Console.WriteLine($"Closing Door...");

            while (currentFloor != targetFloor)
            {
                if (Status == ElevatorStatus.UP)
                {
                    currentFloor++;
                    Console.WriteLine($"Moving UP... Currently at floor {currentFloor}");
                }
                else if (Status == ElevatorStatus.DOWN)
                {
                    currentFloor--;
                    Console.WriteLine($"Moving Down... Currently at floor {currentFloor}");
                }
            }

            Stop(currentFloor);
        }

        public int EnterPassengers()
        {
            int numberOfPeople = 0;

            while (numberOfPeople <= 0)
            {
                Console.Write("How many people are inside the lift? ");
                if (int.TryParse(Console.ReadLine(), out int input) && input > 0)
                {
                    numberOfPeople = input;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number of people.");
                }
            }

            return numberOfPeople * averagePersonWeight;
        }
    }
}
