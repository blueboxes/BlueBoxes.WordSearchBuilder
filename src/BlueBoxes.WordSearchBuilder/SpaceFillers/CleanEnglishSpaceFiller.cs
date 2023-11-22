using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.WordPlacers;

namespace BlueBoxes.WordSearchBuilder.SpaceFillers;

/// <summary>
/// Fills in spaces try it's best to prevent 
/// </summary>
public class CleanEnglishSpaceFiller : DefaultEnglishSpaceFiller
{
    public IEnumerable<string> ProhibitedWords { get; } = Array.Empty<string>();

    public StandardWordSearchSolver WordSearchSolver { get; } = new StandardWordSearchSolver();

    public CleanEnglishSpaceFiller(IEnumerable<string> prohibitedWords)
    {
        ProhibitedWords = prohibitedWords;
    }

    public override char[][] FillSpacesInGrid(char[][] grid)
    {
        var originalGrid = grid.DeepClone();

        grid = base.FillSpacesInGrid(grid);

        foreach (var word in ProhibitedWords)
        {
            var findWords = WordSearchSolver.SeekWord(grid, word);
            if (findWords.Any())
            {
                foreach (var foundWord in findWords)
                {
                    for (int letterIndex = 0; letterIndex < foundWord.GridWordLength; letterIndex++)
                    {
                        var currentRow = foundWord.StartCell.Row + letterIndex * foundWord.Direction.ToRowDelta();
                        var currentCol = foundWord.StartCell.Col + letterIndex * foundWord.Direction.ToColDelta();

                        var currentChar = grid[currentCol][currentRow];
                        var originalChar = originalGrid[currentCol][currentRow];

                        //Letter is not part of a puzzle word
                        if (originalChar == WordPlacer.NullChar)
                        {
                            grid[currentCol][currentRow] = GetNewRandomLetter(currentChar);
                        }
                    }
                }
            }
        }

        return grid;
    }

    private char GetNewRandomLetter(char currentLetter)
    {
        var newLetter = currentLetter;

        while (newLetter == currentLetter)
        {
            newLetter = GetWeightedRandomLetter();
        }
        return newLetter;
    }


}
