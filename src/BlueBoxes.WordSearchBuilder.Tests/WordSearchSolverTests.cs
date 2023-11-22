using BlueBoxes.WordSearchBuilder.Models;
using NUnit.Framework.Internal;

namespace BlueBoxes.WordSearchBuilder.Tests;

public class WordSearchSolverTests
{
    private char[][] _puzzleGrid = new char[][]
    {
        new char[] {'A', 'D', 'G', 'T', 'T', 'G', 'G', 'O', 'R', 'F'},
        new char[] {'B', 'P', 'X', 'A', 'H', 'A', 'H', 'H', 'H', 'D'},
        new char[] {'C', 'F', 'I', 'O', 'I', 'I', 'C', 'I', 'I', 'O'},
        new char[] {'C', 'F', 'T', 'G', 'F', 'I', 'I', 'I', 'A', 'G'},
        new char[] {'C', 'A', 'N', 'I', 'I', 'I', 'I', 'I', 'P', 'I'},
        new char[] {'B', 'F', 'I', 'I', 'I', 'D', 'I', 'B', 'E', 'B'},
        new char[] {'C', 'F', 'I', 'I', 'T', 'O', 'I', 'I', 'I', 'C'},
        new char[] {'C', 'F', 'I', 'I', 'I', 'G', 'N', 'R', 'I', 'O'},
        new char[] {'C', 'F', 'I', 'I', 'I', 'A', 'D', 'I', 'I', 'W'},
        new char[] {'C', 'F', 'I', 'I', 'W', 'O', 'R', 'M', 'I', 'I'}
    };

    [Test]
    public void TestWhenWordIsNotPresent()
    {
        var solver = new StandardWordSearchSolver();

        var result = solver.SeekWord(_puzzleGrid, "mouse");
        result?.Should().BeEmpty();
    }

    //find two items when word is in the grid twice
    [Test]
    public void TestWhenWordIsPresentTwice()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> {
            new PlacedWord("dog", 3, Direction.East, new GridCell(5, 5)),
            new PlacedWord("dog", 3, Direction.East, new GridCell(1, 9))
        };

        var result = solver.SeekWord(_puzzleGrid, "dog");
        result?.Should().HaveCount(2);
        result.Should().BeEquivalentTo(expectedLocations, "dog should be found at the location in the grid");
    }


    //find the word east to west
    [Test]
    public void TestWhenWordIsPresentWest()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("goat", 4, Direction.West, new GridCell(3, 3)) };

        var result = solver.SeekWord(_puzzleGrid, "goat");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "goat should be found at the location in the grid");
    }

    //find the word bottom to top
    [Test]
    public void TestWhenWordIsPresentNorth()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("frog", 4, Direction.North, new GridCell(0, 9)) };

        var result = solver.SeekWord(_puzzleGrid, "frog");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "frog should be found at the location in the grid");
    }

    //find the word top to bottom
    [Test]
    public void TestWhenWordIsPresentSouth()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("worm", 4, Direction.South, new GridCell(9, 4)) };

        var result = solver.SeekWord(_puzzleGrid, "worm");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "worm should be found at the location in the grid");
    }

    //test word bottom left to top right
    [Test]
    public void TestWhenWordIsPresentNorthEast()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("bird", 4, Direction.NorthEast, new GridCell(5, 9)) };

        var result = solver.SeekWord(_puzzleGrid, "bird");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "bird should be found at the location in the grid");
    }

    //test word top right to bottom left 
    [Test]
    public void TestWhenWordIsPresentSouthWest()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("bat", 3, Direction.SouthWest, new GridCell(5, 0)) };

        var result = solver.SeekWord(_puzzleGrid, "bat");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "bat should be found at the location in the grid");
    }

    //test word top right to ledt
    [Test]
    public void TestWhenWordIsPresentWithSpecialChar()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("ba_t", 3, Direction.SouthWest, new GridCell(5, 0)) };

        var result = solver.SeekWord(_puzzleGrid, "ba_t");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "ba_t should be found at the location in the grid");
    }

    //test word top right to ledt
    [Test]
    public void TestWhenWordIsPresentEast()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("ape", 3, Direction.East, new GridCell(3, 8)) };

        var result = solver.SeekWord(_puzzleGrid, "ape");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "ape should be found at the location in the grid");
    }

    //test word bottom right to top left
    [Test]
    public void TestWhenWordIsPresentNorthWest()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("fox", 3, Direction.NorthWest, new GridCell(3, 4)) };

        var result = solver.SeekWord(_puzzleGrid, "fox");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "fox should be found at the location in the grid");
    }

    //test word top left to bottom right
    [Test]
    public void TestWhenWordIsPresentSouthEast()
    {
        var solver = new StandardWordSearchSolver();
        var expectedLocations = new List<PlacedWord> { new PlacedWord("pig", 3, Direction.SouthEast, new GridCell(1, 1)) };

        var result = solver.SeekWord(_puzzleGrid, "pig");
        result?.Should().HaveCount(1);
        result.Should().BeEquivalentTo(expectedLocations, "pig should be found at the location in the grid");
    }


}


