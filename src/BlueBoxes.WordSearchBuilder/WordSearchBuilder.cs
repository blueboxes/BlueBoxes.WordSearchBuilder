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
    public string Title { get; private set; } = "";

    private List<WordPlacer> WordPlacers { get; set; }

    public ISpaceFiller SpaceFiller { get; private set; } = new DefaultEnglishSpaceFiller();

    /// <summary>
    /// Create a new WordSearchBuilder with a default set of Medium Difficulty WordPlacers
    /// </summary>
    /// <param name="width">Grid Width</param>
    /// <param name="height">Grid Height</param>
    public WordSearchBuilder(int width, int height)
    {
        Grid = GridExtensions.Initialize(width, height, WordPlacer.NullChar);
        WordPlacers = PlacerSets.GetSet(Difficulty.Medium);
    }

    /// <summary>
    /// Sets the difficulty of the WordSearchBuilder
    /// </summary>
    /// <param name="difficulty">Difficulty Level</param>
    /// <returns>Current WordSearchBuilder</returns>
    public WordSearchBuilder WithDifficulty(Difficulty difficulty)
    {
        WordPlacers = PlacerSets.GetSet(difficulty);
        return this;
    }

    /// <summary>
    /// Sets the Title of the WordSearch Puzzle
    /// </summary>
    /// <param name="title">Title</param>
    /// <returns>Current WordSearchBuilder</returns>

    public WordSearchBuilder WithTitle(string title)
    {
        Title = title;
        return this;
    }
     

    /// <summary>
    /// Sets the SpaceFiller of the WordSearchBuilder
    /// </summary>
    /// <param name="spaceFiller">SpaceFiller</param>
    /// <returns>Current WordSearchBuilder</returns>
    public WordSearchBuilder WithSpaceFiller(ISpaceFiller spaceFiller)
    {
        SpaceFiller = spaceFiller;
        return this;
    }

    /// <summary>
    /// Sets a custom set of WordPlacers
    /// </summary>
    /// <param name="wordPlacers">Set of Word Placers to use</param>
    /// <returns>Current WordSearchBuilder</returns>
    public WordSearchBuilder WithWordPlacers(IEnumerable<WordPlacer> wordPlacers)
    {
        WordPlacers.AddRange(wordPlacers);
        return this;
    }

    /// <summary>
    /// Adds a set of words to the WordSearchBuilder grid. Words are sorted by complexity before being added.
    /// </summary>
    /// <param name="words">List of words to add</param>
    /// <returns>Current WordSearchBuilder</returns>
    public WordSearchBuilder WithWords(params string[] words)
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
    /// <returns>Complete PuzzleDefinition in iPuz format</returns>
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
            Publisher = "BlueBox Puzzles",
            Puzzle = Grid
        };

        return iPuz;
    }
}


