using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Mars_Rover.Input
{
    internal class Coordinate
    {
         internal int x { get; set; }
         internal int y { get; set; }

        public Coordinate(int row, int column)
        {
            x = row;
            y = column;
        }
    }
}
