using Mars_Rover.Input;

namespace Mars_Rover.Logic
{
    internal interface IMissionControl
    {
        //internal static int DeployedRoverCount = 0;
         int MaxRoverCount { get; }
         List<Rover> RoversDeployed { get;  set; }
         Rover currentRover { get;  set; }
         Plateau plateau { get; set; }
         List<Coordinate> roverLocations { get; set; }
         void createPlateau(PlateauSize size);
         Rover DeployRover(string roverName, string description);
         int getDeployedRoverCount();
         void SelectCurrentRover(int selectedRoverIndex);
         ExecutionStatus ChangeRoverLocation(Rover rover, Position newPosition);
         ExecutionStatus MoveRover(List<Instructions> instructions);
    }
}