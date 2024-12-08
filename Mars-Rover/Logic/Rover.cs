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

        internal ExecutionStatus Move(PlateauSize size)
        {
           
            int currentPointX = roverPosition.point.x;
            int currentPointY = roverPosition.point.y;
            Directions currentlyFacing = roverPosition.facing;
            Coordinates newCoordinate = roverPosition.facing switch
            {
                Directions.N => new Coordinates(currentPointX, currentPointY + 1),
                Directions.E => new Coordinates(currentPointX + 1, currentPointY),
                Directions.S => new Coordinates(currentPointX, currentPointY - 1),
                Directions.W => new Coordinates(currentPointX - 1, currentPointY )
            };
            bool canMove = IsValidCoordinate(newCoordinate, size);
            if (canMove)
            {                 
                roverPosition = new Position(newCoordinate.x, newCoordinate.y, currentlyFacing);
                return ExecutionStatus.SUCCESS;
            }
            return ExecutionStatus.FAILURE;
            
        }

        private bool IsValidCoordinate(Coordinates newCoordinate, PlateauSize plateauSize)
        {
            if ((newCoordinate.x < 0) || (newCoordinate.y < 0) )
                return false;
            
            if ((newCoordinate.x > plateauSize.Columns) || (newCoordinate.y > plateauSize.Rows) )
                return false;
          return true;
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
    }
}
