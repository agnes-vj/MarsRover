using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mars_Rover.Input;
namespace Mars_Rover.Logic
{
    internal sealed class Plateau
    {
         private static Plateau _plateau = null;
        private static readonly object _lock = new object();
        internal PlateauSize size {  get;}

        private Plateau(PlateauSize size)
        {
            this.size = size;
        }
        public static Plateau Create(PlateauSize size)
        {

          if (_plateau == null)
           {
                lock (_lock)
                {
                    if (_plateau == null)
                    {
                        _plateau = new Plateau(size);
                    }                   
                      
                }
           }
            return _plateau;
        }


    }
}
