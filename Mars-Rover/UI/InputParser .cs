using Mars_Rover.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mars_Rover.Input
{
    internal class InputParser
    {
        public static PlateauSize DefinePlateauSize(string rows, string columns)
        {
            int rowSize = ParseValidPositiveNumber(rows);
            int columnSize = ParseValidPositiveNumber(columns);

            if (rowSize >= 0 && columnSize >= 0)
            {
                return new PlateauSize(rowSize, columnSize);
            }
            else
                return null;
        }
        public Position SetPosition(int x, int y, Directions facing)
        {

            return new Position(x, y, facing);
        }
        public static Position GetRoverInitialPosition()
        {

            return null;
        }
        static int ParseValidPositiveNumber(string number)
        {
            int validNumber;
            bool isNumber = int.TryParse(number, out validNumber);
            if (isNumber)
            {
                if (validNumber >= 0)
                    return validNumber;
            }
            Console.WriteLine(validNumber);
            return -1;
        }

        internal static Position ParseRoverPosition(string roverPositionString)
        {
            int x;
            int y;


            if (string.IsNullOrWhiteSpace(roverPositionString))
                return null;
           
            roverPositionString = Regex.Replace(roverPositionString.Trim(), @"\s+", " ");          


            // Split input into array of values
            string[] input = roverPositionString.Split(' ');
            if (input.Length != 3)
                return null;

            // Parse x and y coordinates
            if (!int.TryParse(input[0], out  x))
                return null;

            if (!int.TryParse(input[1], out  y))
                return null;

            if (x < 0 || y < 0)
                return null;

            // Parse direction
            if (!Enum.TryParse(typeof(Directions), input[2], true, out var facing) ||
                !Enum.IsDefined(typeof(Directions), facing))
                return null;

            // Return the valid Position object
            return new Position(x, y, (Directions)facing);
        }

        internal static List<Instructions> ParseInstructions(string instructionString)
        {
            if (string.IsNullOrWhiteSpace(instructionString) || string.IsNullOrEmpty(instructionString))
                return null;
            List<Instructions> result = new List<Instructions>();
            foreach(char ltr in instructionString) 
            {  
               if (!Enum.TryParse(typeof(Instructions), ltr.ToString(), true, out var instructionChar) ||
                      !Enum.IsDefined(typeof(Instructions), instructionChar))
                    return null;
                else
                    result.Add((Instructions)instructionChar);

            }
            return result;
        }
        //public void DefineNumberOfRovers()
        //{
        //    int roverCount;
        //    Console.Write("Enter Number of Rovers To Deploy : ");
        //    roverCount = UserInput.getValidInteger();
        //    MissionControl.
        //}
    }

}