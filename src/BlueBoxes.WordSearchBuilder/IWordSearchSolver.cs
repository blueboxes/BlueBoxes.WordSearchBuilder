using BlueBoxes.WordSearchBuilder.Models;

namespace BlueBoxes.WordSearchBuilder;

public interface IWordSearchSolver
{
    IReadOnlyCollection<PlacedWord> SeekWord(char[][] puzzleGrid, string word);
}
