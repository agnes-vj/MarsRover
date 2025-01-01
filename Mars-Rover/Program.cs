// See https://aka.ms/new-console-template for more information

using Mars_Rover.Input;
using Mars_Rover;
using Mars_Rover.Logic;
using System.Drawing;

MissionControl missionControl = new MissionControl();
UserInput userInput = new UserInput(missionControl);
userInput.DisplayOptions();


// Move Rover**************
//missionControl.createPlateau(plateauSize);
//rover1 = missionControl.DeployRover(roverPosition);
//ExecutionStatus status = missionControl.MoveRover(rover1, instructions);
//Console.Write("Rover is now at : ");
//Console.Write(rover1.roverPosition.point.x);
//Console.Write(rover1.roverPosition.point.y);
//Console.Write(rover1.roverPosition.facing);