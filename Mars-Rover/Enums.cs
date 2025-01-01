using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover
{

       internal enum Instructions
        {
            L,
            R,
            M
        }

       internal enum Directions
        {
            N,
            E,
            S,
            W            
        }
    internal enum ExecutionStatus
    {
        SUCCESS,
        FAILURE,
        OUT_OF_RANGE,
        POSITION_OCCUPIED,
        PLATEAU_NOT_CREATED
    }
    
}
