using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover.Input
{
    internal static class UserInput
    {
        public static int getValidInteger()
        {
            int inputInteger;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out inputInteger))
                    Console.WriteLine("Error: Enter an integer");
                else
                    break;
            }
            return inputInteger;
        }
        internal static void DisplayOptions()
        {
            Console.WriteLine("1. Deploy a new Rover");
            Console.WriteLine("2. Make a move");
        }
    }
}
