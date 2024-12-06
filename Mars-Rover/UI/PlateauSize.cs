using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover.Input
{
    internal class PlateauSize
    {
        public int Rows { get; }
        public int Columns { get;}

        public PlateauSize(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
        }
    }
}
