using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover.Logic
{
    internal class MissionControl
    {
        internal static int DeployedRoverCount;
        internal static int MaxRoverCount = int.TryParse(Environment.GetEnvironmentVariable("MAX_ROVER_COUNT"), out var roverCount) ? roverCount : 5;
        List<Rover> RoversDeployed = new();


    }
}
