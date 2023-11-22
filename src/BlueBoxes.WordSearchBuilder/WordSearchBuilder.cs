using BlueBoxes.WordSearchBuilder.WordPlacers;
using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.SpaceFillers;

namespace BlueBoxes.WordSearchBuilder;

/// <summary>
/// Primary entrypoint to build word search grids
/// </summary>
public class WordSearchBuilder
{
    /// <summary>
    /// Grid of Words Referenced with [Col][Row]
    /// </summary>
    public char[][] Grid { get; private set; }
    public int Width => Grid.Width();
    public int Height => Grid.Height();
    public List<PlacedWord> Solution { get; } = new List<PlacedWord>();
    public Difficulty DifficultyLevel { get; }
    public string? Notes { get; set; }
    public string Title { get; private set; }


    //Default versions that can be replaced
    public List<WordPlacer> WordPlacers { get; set; }

    public ISpaceFiller SpaceFiller { get; private set; } = new DefaultEnglishSpaceFiller();

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public WordSearchBuilder(int width, int height, string title = "", Difficulty difficulty = Difficulty.Easy)
    {
        Grid = GridExtensions.Initialize(width, height, WordPlacer.NullChar);
        Title = title;
        DifficultyLevel = difficulty;
        WordPlacers = PlacerSets.GetSet(difficulty);
    }

    public WordSearchBuilder SetSpaceFiller(ISpaceFiller spaceFiller)
    {
        SpaceFiller = spaceFiller;
        return this;
    }
    public WordSearchBuilder SetWordPlacers(IEnumerable<WordPlacer> wordPlacers)
    {
        WordPlacers.AddRange(wordPlacers);
        return this;
    }

    public WordSearchBuilder AddWords(params string[] words)
    {
        foreach (var currentWord in words.SortByComplexity())
        {
            foreach (var placer in WordPlacers.Shuffle())
            {
                var result = placer.TryPlaceWord(currentWord, Grid);
                if (result != PlacedWord.Empty)
                {
                    Solution.Add(result);
                    break;
                }
            }
        }

        Solution.Sort((a, b) => string.Compare(a?.Word, b?.Word, false, System.Globalization.CultureInfo.CurrentCulture));
        return this;
    }


    /// <summary>
    /// Based on iPuz format create a PuzzleDefinition
    /// </summary>
    /// <returns></returns>
    public PuzzleDefinition Build()
    {
        SpaceFiller.FillSpacesInGrid(Grid);

        var iPuz = new PuzzleDefinition
        {
            Uniqueid = Guid.NewGuid().ToString(),
            Dimensions = new GridDimension() { Width = Width, Height = Height },
            Title = Title,
            Notes = Notes,
            Date = DateTime.UtcNow,
            Difficulty = DifficultyLevel.ToString(),
            Solution = Solution,
            Copyright = DateTime.Now.Year.ToString(),
            Publisher = "Bluebox Puzzles",
            Puzzle = Grid
        };

        return iPuz;
    }
}


