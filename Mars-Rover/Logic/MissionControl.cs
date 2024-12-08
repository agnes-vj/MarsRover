using Mars_Rover.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover.Logic
{
    internal class MissionControl
    {
        internal static int DeployedRoverCount = 0;
        internal static int MaxRoverCount = int.TryParse(Environment.GetEnvironmentVariable("MAX_ROVER_COUNT"), out var roverCount) ? roverCount : 5;
        List<Rover> RoversDeployed = new();

        Plateau plateau;
        Rover rover;
        List<Coordinates> roverLocations = new();

        public void createPlateau(PlateauSize size)
        {
            plateau = Plateau.Create(size);
        }
        public Rover DeployRover(Position roverPosition)
        {
            if (DeployedRoverCount <= MaxRoverCount)
            {
                DeployedRoverCount++;
                Rover newRover = new Rover($"Rover{DeployedRoverCount}", "Rover to explore Mars", roverPosition);
                RoversDeployed.Add(newRover);
                return newRover;
            }
            return null;
        }

        public ExecutionStatus MoveRover(Rover rover, List<Instructions> instructions)
        {
            ExecutionStatus status = ExecutionStatus.FAILURE;

            foreach (Instructions instruction in instructions) 
            {
                switch (instruction)
                {
                    case Instructions.M:
                           status = rover.Move(plateau.size);
                           if (status == ExecutionStatus.FAILURE)
                                return ExecutionStatus.FAILURE;
                           break;
                    case Instructions.L:
                            status = rover.Turn(Instructions.L);
                            if (status == ExecutionStatus.FAILURE)
                                return ExecutionStatus.FAILURE;
                            break;
                    case Instructions.R:
                            status = rover.Turn(Instructions.R);
                            if (status == ExecutionStatus.FAILURE)
                                return ExecutionStatus.FAILURE;
                            break;
                 }
            }
            return status;
        }
    }
}
