using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.Helpers;

namespace BlueBoxes.WordSearchBuilder.WordPlacers;

public class Reversed : WordPlacer
{
    private readonly WordPlacer _placer;

    public Reversed(WordPlacer placer)
    {
        _placer = placer;
    }

    public override PlacedWord TryPlaceWord(string word, char[][] grid)
    {
        var wordPlaced = _placer.TryPlaceWord(string.Join("", word.Reverse()), grid);

        if (wordPlaced != PlacedWord.Empty)
        {
            var endCol = wordPlaced.StartCell.Col - Direction.ToColDelta() * (wordPlaced.GridWordLength - 1);
            var endRow = wordPlaced.StartCell.Row - Direction.ToRowDelta() * (wordPlaced.GridWordLength - 1);
            var endCell = new GridCell(endCol, endRow);
            return new PlacedWord(word, wordPlaced.GridWordLength, Direction, endCell);
        }

        return wordPlaced ?? PlacedWord.Empty;
    }

    public Direction GetReversedDirection()
    {
        return _placer.Direction switch
        {
            Direction.None => Direction.None,
            Direction.North => Direction.South,
            Direction.NorthEast => Direction.SouthWest,
            Direction.East => Direction.West,
            Direction.SouthEast => Direction.NorthWest,
            Direction.South => Direction.North,
            Direction.SouthWest => Direction.NorthEast,
            Direction.West => Direction.East,
            Direction.NorthWest => Direction.SouthEast,
            _ => throw new ArgumentException("Invalid enum value for command", nameof(_placer.Direction)),
        };
    }

    public override Direction Direction => GetReversedDirection();
}

