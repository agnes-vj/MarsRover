using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mars_Rover.Input;
namespace Mars_Rover.Logic
{
    internal class Rover
    {
        internal string roverName { get; set; }
        internal string roverInfo{ get; set; }
        internal Position roverPosition { get; set; }

        public Rover(string roverName, string roverInfo, Position position)
        {
            this.roverName = roverName;
            this.roverInfo = roverInfo;
            this.roverPosition = position;
        }

        internal ExecutionStatus Move(PlateauSize size, List<Coordinate> roverLocations)
        {
           
            int currentPointX = roverPosition.point.x;
            int currentPointY = roverPosition.point.y;            
            Directions currentlyFacing = roverPosition.facing;
            List<Coordinate> roverLocationsExceptCurrent = roverLocations.Where(location => (location.x != currentPointX && location.y != currentPointY)).ToList();
            roverLocationsExceptCurrent.ForEach(item => Console.WriteLine($"occupied x : {item.x}, y: {item.y}"));
            Coordinate newCoordinate = roverPosition.facing switch
            {
                Directions.N => new Coordinate(currentPointX, currentPointY + 1),
                Directions.E => new Coordinate(currentPointX + 1, currentPointY),
                Directions.S => new Coordinate(currentPointX, currentPointY - 1),
                Directions.W => new Coordinate(currentPointX - 1, currentPointY )
            };
            
            ExecutionStatus status = IsValidCoordinate(newCoordinate, size, roverLocations);
            if (status == ExecutionStatus.SUCCESS)
              roverPosition = new Position(newCoordinate.x, newCoordinate.y, currentlyFacing);
            
            return status;
            
        }

        internal static ExecutionStatus IsValidCoordinate(Coordinate newCoordinate, PlateauSize plateauSize, List<Coordinate> roverLocations)
        {
            if ((newCoordinate.x < 0) || (newCoordinate.y < 0) )
                return ExecutionStatus.OUT_OF_RANGE;
            
            if ((newCoordinate.x > plateauSize.Columns) || (newCoordinate.y > plateauSize.Rows) )
                return ExecutionStatus.OUT_OF_RANGE;
            if (roverLocations.Any(coord => coord.x == newCoordinate.x && coord.y == newCoordinate.y))
                return ExecutionStatus.POSITION_OCCUPIED;
          return ExecutionStatus.SUCCESS;
        }

        internal ExecutionStatus Turn(Instructions leftOrRight)
        {
            Instructions[] allowedInstructions = { Instructions.L, Instructions.R };
            if (!allowedInstructions.Contains(leftOrRight))
                return ExecutionStatus.FAILURE;
            roverPosition.facing = leftOrRight switch
            {
                Instructions.L => roverPosition.facing switch
                {
                    Directions.N => Directions.W,
                    Directions.W => Directions.S,
                    Directions.S => Directions.E,
                    Directions.E => Directions.N,
                },
                Instructions.R => roverPosition.facing switch
                {
                    Directions.N => Directions.E,
                    Directions.E => Directions.S,
                    Directions.S => Directions.W,
                    Directions.W => Directions.N,
                }
            };

            return ExecutionStatus.SUCCESS;
        }

        internal void returnToBase()
        {

        }

        internal ExecutionStatus ChangeLocation(PlateauSize size, List<Coordinate> roverLocations, Position newPosition)
        {
            Coordinate newCoordinate = new(newPosition.point.x, newPosition.point.y);
            ExecutionStatus status = IsValidCoordinate(newCoordinate, size, roverLocations);
            if (status == ExecutionStatus.SUCCESS)
                roverPosition = new Position(newPosition.point.x, newPosition.point.y, newPosition.facing);

            return status;
        }

        internal Rover clone()
        {
            return new Rover(roverName,roverInfo, new Position(roverPosition.point.x, roverPosition.point.y, roverPosition.facing));
        }
    }
}
