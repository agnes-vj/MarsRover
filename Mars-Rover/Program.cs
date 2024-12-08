// See https://aka.ms/new-console-template for more information

using Mars_Rover.Input;
using Mars_Rover;
using Mars_Rover.Logic;
using System.Drawing;


PlateauSize plateauSize = null;
Position roverPosition = null;
List<Instructions> instructions = null;

Plateau plateau;
Rover rover1;
MissionControl missionControl = new();
//Input *******************
string rows;
string columns;
bool isValidInput = false;
while(!isValidInput)
{
    Console.WriteLine("Enter the size of the Plateau ");
    Console.Write("Number of Rows : ");
    rows = Console.ReadLine();
    Console.Write("Number of Columns : ");
    columns = Console.ReadLine();

    plateauSize = InputParser.DefinePlateauSize(rows,columns);
    if (plateauSize != null)
        isValidInput = true;
    else
        Console.WriteLine("Error : Enter a valid positive number");
}
string roverPositionString;
isValidInput = false;
while (!isValidInput)
{
    Console.WriteLine("Enter Rover's position now");
    roverPositionString = Console.ReadLine();

    roverPosition = InputParser.ParseRoverPosition(roverPositionString);
    if (roverPosition != null)
        isValidInput = true;
    else
        Console.WriteLine("Error : Enter a valid Rover Position");
}

isValidInput = false;
while (!isValidInput)
{
    Console.WriteLine("Enter Rover Moving Instruction :");
    string instructionString = Console.ReadLine();
    instructions = InputParser.ParseInstructions(instructionString);
    Console.WriteLine();
    if (instructions != null)
        isValidInput = true;
    else
        Console.WriteLine("Error : Enter a valid Rover Moving Instruction");
}

// Move Rover**************
missionControl.createPlateau(plateauSize);
rover1 = missionControl.DeployRover(roverPosition);
ExecutionStatus status = missionControl.MoveRover(rover1, instructions);

Console.Write(rover1.roverPosition.point.x);
Console.Write(rover1.roverPosition.point.y);
Console.Write(rover1.roverPosition.facing);





//Position roverPosition = InputParser.GetRoverInitialPosition();



