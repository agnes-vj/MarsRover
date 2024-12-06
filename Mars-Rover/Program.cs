// See https://aka.ms/new-console-template for more information

using Mars_Rover.Input;
using Mars_Rover;


PlateauSize plateauSize;
Position roverPosition;
List<Instructions> instructions;
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
Console.ReadLine();




//Position roverPosition = InputParser.GetRoverInitialPosition();



