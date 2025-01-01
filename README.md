The Mars Rover 
  This c# project aims to deploy and move rovers on a defined plateau.
Plateau
  The surface of Mars is represented by a Plateau. It is a square/rectangular grid.
Instructions to move Rover
  Rovers navigate the Plateau by following a sequence of Instructions. They can also use their cameras and robot arms to collect photographs and samples.
The Rover position
  A Rover's position is represented by x and y co-ordinates and the letters N, S, W, E to represent North, South, West, East (the four cardinal compass points) respectively.

  Example
    0 0 N
    This means the Rover is at the bottom-left corner facing in the North direction.
    Assume that the square directly North from (x, y) is (x, y + 1), and the square directly East from (x, y) is (x + 1, y)

Program Inputs

First Line: Plateau Creation
The first line inputted into the program represents the upper-right coordinates of the Plateau.

Example:

5 5
This Plateau has maximum (x, y) co-ordinates of (5, 5), and is therefore a Plateau of size (6,6).

Assume that the lower-left coordinate is (0, 0).

Subsequent lines: Rover Creation & Instructions
Following the plateau creation, each Rover receives two lines of input. The first line of input is to land the Rover at a particular starting position, e.g.

1 2 N
This lands the Rover at position (1,2) facing North.

The following line of input is a string of letters representing instructions to move the Rover around the Plateau.

Instructions
To move a Rover around the Plateau, a string of letters is sent to a Rover.

Letter	Action
L	Spins the Rover 90 degrees Left without moving from the current coordinate point
R	Spins the Rover 90 degrees Right without moving from the current coordinate point
M	Moves the Rover forward by one grid point, maintaining the same heading/orientation
Output
For each Rover, the output represents its final position: the coordinates and where it is facing.

Movement Rules
Rovers move sequentially, this means that if multiple Rovers are being landed then the first Rover needs to finish moving first before the next one can move.

Example
Given the following input:

5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
This creates a plateau of size (5,5) and lands two rovers on it. The first lands at (1,2)N and this one moves LMLMLMLMM. The second lands at (3,3)E and it moves MMRMMRMRRM.

The expected output is:

1 3 N
5 1 E
