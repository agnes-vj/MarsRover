using FluentAssertions;
using Mars_Rover.Input;
using Mars_Rover;
using System.Text.RegularExpressions;
namespace TestMarsRover
{
    public class Tests
    {



        [TestCase("0", "2")]
        [TestCase("1", "0")]
        [TestCase("5", "8")]
        public void SetPlateauSizeValidTest(string x, string y)
        {
            //Arrange
            int xIntValue = int.Parse(x);           
            int yIntValue = int.Parse(y);
            PlateauSize expected = new(xIntValue, yIntValue);


            //Assign
            PlateauSize result = InputParser.DefinePlateauSize(x, y);

            //Assert

            result.Rows.Should().Be(expected.Rows);
            result.Columns.Should().Be(expected.Columns);
        }

        [TestCase("-1", "2")]
        [TestCase("1", "-3")]
        [TestCase("-1", "-2")]
        public void SetPlateauSizeNegativeTest(string x, string y)
        {
            //Arrange
            PlateauSize expected = null;
            
            //Assign
            PlateauSize result = InputParser.DefinePlateauSize(x, y);

            //Assert
            result.Should().Be(expected);            
        }


        [TestCase("1 2 W  ")]
        [TestCase("15   2 E")]
        [TestCase("  0 20 N")]
        [TestCase("4 0   s")]
        public void ParseRoverPositionValidTest(string input)
        {
            //Arrange
            input = Regex.Replace(input.Trim(), @"\s+", " ");
            Console.WriteLine(input);
            int x = int.Parse(input.Split(' ')[0]);
            int y =  int.Parse(input.Split(' ')[1]);
            Enum.TryParse(typeof(Directions), input.Split(' ')[2], true, out var facing);
            Position expected = new Position(x, y, (Directions) facing);

            //Act
            Position result = InputParser.ParseRoverPosition(input);

            //Asset
            result.point.x.Should().Be(expected.point.x);
            result.point.y.Should().Be(expected.point.y);
            result.facing.Should().Be(expected.facing);

        }
        [TestCase("-1 2 W")]
        [TestCase("15 2 G")]
        [TestCase("0 -20 N")]
        [TestCase("4 G S")]
        [TestCase("-2 4 S")]
        public void ParseRoverPositionInvalidTest(string input)
        {
            //Arrange
            
            Position expected = null;

            //Act
            Position result = InputParser.ParseRoverPosition(input);

            //Asset

            result.Should().Be(expected);

        }

        [TestCase("LMLMM")]

        public void ParseInstructionsValidTest(string inputString)
        {
            //Arrange
            char[] input = inputString.ToCharArray();
            List<Instructions> expected = new() { Instructions.L, Instructions.M, Instructions.L, Instructions.M, Instructions.M };

            //Act
            List<Instructions> result = InputParser.ParseInstructions(inputString);


            //Assert
            result.Should().BeEquivalentTo(expected);
           // Assert.IsTrue(result?.SequenceEqual(expected));
        }
        [TestCase("LMlM")]

        public void ParseInstructionsValidLowercaseTest(string inputString)
        {
            //Arrange
            char[] input = inputString.ToCharArray();
            List<Instructions> expected = new() { Instructions.L, Instructions.M, Instructions.L, Instructions.M};

            //Act
            List<Instructions> result = InputParser.ParseInstructions(inputString);


            //Assert
            result.Should().BeEquivalentTo(expected);
            // Assert.IsTrue(result?.SequenceEqual(expected));


        }

        [TestCase("")]
        [TestCase("LKLGM")]
        [TestCase("LKL1M")]
        [TestCase(" ")]
        [TestCase("-1LKLGM")]
        [TestCase("LKL1M")]
        [TestCase("L KLM")]



        public void ParseInstructionsInvalidTest(string inputString)
        {
            //Arrange
            
            List<Instructions> expected = null;

            //Act
            List<Instructions> result = InputParser.ParseInstructions(inputString);


            //Assert
            Assert.IsTrue(result == expected);


        }
    }
}