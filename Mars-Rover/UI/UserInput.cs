using Mars_Rover.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars_Rover.Input
{
    internal class UserInput
    {
       internal IMissionControl missionControl;
        public UserInput(IMissionControl missionControl)
        {
            this.missionControl = missionControl;
        }
        public int getValidInteger()
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
        internal void DisplayOptions()
        {
            //Console.Clear();
            Console.WriteLine("1. Define Plateau Size");   
            Console.WriteLine("2. Deploy a new Rover");
            Console.WriteLine("3. Show Rover position");
            Console.WriteLine("4. Change Rover Positions");
            Console.WriteLine("5. Move a Rover");
            Console.WriteLine("6. Quit");
            Console.Write("Selection an Option :");
            string? optionSelected = Console.ReadLine();
            DoOption(optionSelected);
        }

        internal void DoOption(string? optionSelected)
        {
            int selectedRoverIndex;
            ExecutionStatus status;
            switch (optionSelected)
            {
               case "1":
                     DefinePlateauSize();
                     Console.WriteLine("Plateau Created.");
                     Thread.Sleep(2000);
                     DisplayOptions();
                     break;
               case "2":
                    Console.WriteLine("Enter New Rover Name : ");
                    string roverName = Console.ReadLine();
                    Console.WriteLine("Enter It's Mission : ");
                    string description = Console.ReadLine();
                    missionControl.DeployRover(roverName, description);
                    Console.WriteLine("Rover Deployed Successfully...");
                    Thread.Sleep(2000);
                    DisplayOptions();
                    break;
                case "3":
                    selectedRoverIndex = SelectRover();
                    missionControl.SelectCurrentRover(selectedRoverIndex);
                    Position currentPosition = missionControl.currentRover.roverPosition;
                    Console.WriteLine($"{missionControl.currentRover.roverName} position");
                    Console.WriteLine("*********************************************");
                    Console.WriteLine($"Row position : {currentPosition.point.x} ");
                    Console.WriteLine($"Column position : {currentPosition.point.y} ");
                    Console.WriteLine($"Facing Direction : {currentPosition.facing} ");
                    Console.Write("Do you want to continue (y/n) : ");
                    string response = Console.ReadLine();
                    if (response.ToLower() != "y")
                        Environment.Exit(0);
                    DisplayOptions();
                    break;
                case "4":
                    selectedRoverIndex = SelectRover();
                    missionControl.SelectCurrentRover(selectedRoverIndex);
                    Position newPosition = getRoverNewPosition();
                    status = missionControl.ChangeRoverLocation(missionControl.currentRover, newPosition);
                    Console.WriteLine($"Rover position change status: {status.ToString()}");
                    Thread.Sleep(2000);
                    DisplayOptions();
                    break;
                case "5":
                    selectedRoverIndex = SelectRover();
                    missionControl.SelectCurrentRover(selectedRoverIndex);
                    List<Instructions> moveInstruction = getMovingInstruction();
                    status = missionControl.MoveRover(moveInstruction);
                    if (status != ExecutionStatus.SUCCESS)
                    {
                        Console.WriteLine($"{status.ToString()} Some of the Instruction can not be performed");
                        Console.WriteLine("Staying in the Original Position");
                    }
                    else
                    {
                        Console.Write("Rover moved to : ");
                        Console.Write(missionControl.currentRover.roverPosition.point.x);
                        Console.Write(missionControl.currentRover.roverPosition.point.y);
                        Console.Write(missionControl.currentRover.roverPosition.facing);
                        Console.WriteLine("Rover Moved Successfully");
                    }
                    Thread.Sleep(2000);
                    DisplayOptions();
                    break;
                case "6":
                    Console.Write("Do you want to quit(y/n): ");
                    string userOption = Console.ReadLine();
                    if (userOption == "y")
                        Environment.Exit(0);
                    else
                        DisplayOptions();
                    break;
                default:
                    break;

            }

        }

        private List<Instructions> getMovingInstruction()
        {
            bool isValidInput = false;
            List<Instructions> instructions = null;
            while (!isValidInput)
            {
                Console.WriteLine("Enter Rover Moving Instruction(space seperated) :");
                Console.WriteLine("R - Turn Right");
                Console.WriteLine("L - Turn Left");
                Console.WriteLine("M - Move 1 Position");
                string instructionString = Console.ReadLine();
                instructions = InputParser.ParseInstructions(instructionString);
                Console.WriteLine();
                if (instructions != null)
                    isValidInput = true;
                else
                    Console.WriteLine("Error : Enter a valid Rover Moving Instruction");
            }
            return instructions;
        }

        private Position getRoverNewPosition()
        {
            string roverPositionString;
            bool isValidInput = false;
            Position? roverPosition = null;

            while (!isValidInput)
            {
                Console.WriteLine("Enter Rover's position to place");
                roverPositionString = Console.ReadLine();

                roverPosition = InputParser.ParseRoverPosition(roverPositionString);
                if (roverPosition != null)
                    isValidInput = true;
                else
                    Console.WriteLine("Error : Enter a valid Rover Position");
            }
            return roverPosition;
        }

        internal void DefinePlateauSize()
        {
            string rows;
            string columns;
            bool isValidInput = false;
            PlateauSize plateauSize = null;
            while (!isValidInput)
            {
                Console.WriteLine("Enter the size of the Plateau ");
                Console.Write("Number of Rows : ");
                rows = Console.ReadLine();
                Console.Write("Number of Columns : ");
                columns = Console.ReadLine();

                plateauSize = InputParser.DefinePlateauSize(rows, columns);
                if (plateauSize != null)
                    isValidInput = true;
                else
                    Console.WriteLine("Error : Enter a valid positive number");
            }
            missionControl.createPlateau(plateauSize);           
        }
        internal void SetRoverPosition()
        {
            
        }
        internal int SelectRover()
        {
            int selectedRoverIndex = -1;
            int roverCount = missionControl.getDeployedRoverCount();
            Console.WriteLine($"Currently {roverCount} are deployed");
            for (int i = 0; i < roverCount; i++)
            {
                Console.WriteLine($"{i + 1}. {missionControl.RoversDeployed[i].roverName}");
            }
            Console.Write("Select Rover : ");
            int.TryParse(Console.ReadLine(), out selectedRoverIndex);
            return selectedRoverIndex - 1;
        }

    }
}
