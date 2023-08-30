using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USSEnterprise.Domain.ValueObjects
{
    public enum ElevatorStatus
    {
        UP,
        DOWN,
        STOPPED,
        MOVING,
        JAMMED,
        OVERWEIGHT,
        SERVICE
    }
}
