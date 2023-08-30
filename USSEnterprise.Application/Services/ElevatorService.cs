using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USSEnterprise.Application.Interfaces;
using USSEnterprise.Domain.Entities;

namespace USSEnterprise.Application.Services
{
    public class ElevatorService : IElevatorService
    {
        private readonly Elevator _elevator;

        public ElevatorService(Elevator elevator)
        {
            this._elevator = elevator;
        }

        public void RequestFloor(int floor)
        {
            
        }
    }
}
