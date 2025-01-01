using Mars_Rover.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mars_Rover.Input
{

    internal class Position
    {
     
        internal Coordinate point { get; set; }
        internal Directions facing { get; set; }

        public Position(int x, int y, Directions facing)
        {
            point = new Coordinate(x,y);
            this.facing = facing;
        }


    }
}
