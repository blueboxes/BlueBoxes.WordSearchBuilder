using Microsoft.AspNetCore.Mvc.RazorPages;
using WordSearchEngine.Models;
using BlueBoxes.WordSearchBuilder;
using static BlueBoxes.WordSearchBuilder;

namespace BlueBoxes.WordSearchBuilder.WebSample.Pages;

public class WebModel : PageModel
{
    public string SetTitle { get; set; } = "";
    public string InputWords { get; set; } = "";
    public Difficulty GridLevel { get; set; } = Difficulty.Easy;

    public Display View { get; set; } = Display.All;
    public IList<PuzzleDefinition> Puzzles { get; set; } = new List<PuzzleDefinition>();
    public PuzzleLayout PuzzleLayout { get; set; } = new EasyPuzzleLayout();

    public enum Display
    {
        All,
        Puzzles,
        Solutions
    }

    public async Task OnGet(string? id, Display view = Display.All)
    {
        if (id != null)
        {
            var repository = new WordSearchEngine.WordSearchRepository();
            var puzzleSet = await repository.LoadSetAsync(id);
            Puzzles = puzzleSet.Puzzles;
            InputWords = string.Join(Environment.NewLine, puzzleSet.Puzzles.Select(a => $"{a.Title},{string.Join(",", a.Solution.Select(b => b.Word))}"));
            GridLevel = Enum.TryParse(puzzleSet.Puzzles.FirstOrDefault()?.Difficulty, out Difficulty difficulty) ? difficulty : Difficulty.Easy;
            SetTitle = puzzleSet.Title ?? string.Empty;
            View = view;

            switch (GridLevel)
            {
                case Difficulty.Medium:
                    PuzzleLayout = new MediumPuzzleLayout();
                    break;
                case Difficulty.Hard:
                    PuzzleLayout = new HardPuzzleLayout();
                    break;
                default:
                    PuzzleLayout = new EasyPuzzleLayout();
                    break;
            }

        }

    }

}
