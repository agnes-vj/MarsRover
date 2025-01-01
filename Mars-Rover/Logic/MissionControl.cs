using Mars_Rover.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover.Logic
{
    internal class MissionControl : IMissionControl
    {
        public int MaxRoverCount { get;} = int.TryParse(Environment.GetEnvironmentVariable("MAX_ROVER_COUNT"), out var roverCount) ? roverCount : 5;
        public List<Rover> RoversDeployed { get; set; } = new();
        public Rover currentRover { get;  set; }
        public Plateau plateau { get; set; } 
        public List<Coordinate> roverLocations { get; set; } = new();

        public void createPlateau(PlateauSize size)
        {
            plateau = Plateau.Create(size);
        }
        public Rover DeployRover(string roverName, string description)
        {
            int deployedRoverCount = getDeployedRoverCount();
            //Console.WriteLine(deployedRoverCount);

            if (deployedRoverCount <= MaxRoverCount)
            {
                Rover newRover = new Rover(roverName, description, new Position(deployedRoverCount, 0, Directions.N));
                RoversDeployed.Add(newRover);
                roverLocations.Add(newRover.roverPosition.point);
                //Console.WriteLine("RoverDeployed@@@@ " + RoversDeployed.Count);
                return newRover;

            }
            return null;
        }

        public void SelectCurrentRover(int selectedRoverIndex)
        {
            currentRover = RoversDeployed[selectedRoverIndex];
        }
        public int getDeployedRoverCount()
        {
            return RoversDeployed.Count;
        }
        public ExecutionStatus ChangeRoverLocation(Rover rover, Position newPosition)
        {
            ExecutionStatus status = ExecutionStatus.FAILURE;
            Coordinate roverCurrentCoordinate = rover.roverPosition.point;

            if (plateau == null)
                return ExecutionStatus.PLATEAU_NOT_CREATED;

            status = rover.ChangeLocation(plateau.size, roverLocations, newPosition);
            if (status != ExecutionStatus.SUCCESS)
                return status;
            roverLocations.Add(rover.roverPosition.point);
            roverLocations.Remove(roverCurrentCoordinate);
            //roverLocations.ForEach(item => Console.WriteLine($"after Move occupied x : {item.x}, y: {item.y}"));

            return ExecutionStatus.SUCCESS;
            
        }

        public ExecutionStatus MoveRover(List<Instructions> instructions)
        {
            int instructionProgressCount = 0;
            Rover currentRoverCopy = currentRover.clone();
            ExecutionStatus status = ExecutionStatus.FAILURE;

            if (plateau == null)
                return ExecutionStatus.PLATEAU_NOT_CREATED;

           // Console.WriteLine("Inside Move Rover, current Rover :" + currentRover.roverName);
            foreach (Instructions instruction in instructions)
            {
                Coordinate roverCurrentPosition = new Coordinate(currentRover.roverPosition.point.x, currentRover.roverPosition.point.y); ;
                switch (instruction)
                {
                    case Instructions.M:

                        status = currentRover.Move(plateau.size, roverLocations);
                        if (status != ExecutionStatus.SUCCESS)
                            break;
                        else
                        {
                            roverLocations.Add(currentRover.roverPosition.point);
                            //Console.WriteLine("Remove " + roverCurrentPosition.x + "  " + roverCurrentPosition.y);
                            roverLocations.RemoveAll(position => (position.x == roverCurrentPosition.x && position.y == roverCurrentPosition.y));
                            roverLocations.ForEach(item => Console.WriteLine($"after Move occupied x : {item.x}, y: {item.y}"));
                            instructionProgressCount++;
                        }
                        break;
                    case Instructions.L:
                        status = currentRover.Turn(Instructions.L);
                        if (status != ExecutionStatus.SUCCESS)
                            break;
                        instructionProgressCount++;
                        break;
                    case Instructions.R:
                        status = currentRover.Turn(Instructions.R);
                        if (status != ExecutionStatus.SUCCESS)
                            break;
                        instructionProgressCount++;
                        break;
                }
                if (status != ExecutionStatus.SUCCESS)
                {
                    currentRover = currentRoverCopy;
                    break;
                }  
            }
            return status;
        }

    }
}
