using Microsoft.AspNetCore.Mvc.RazorPages;
using WordSearchEngine.Models;
using static WordSearchEngine.WordSearchBuilder;

namespace BlueBoxes.WordSearchBuilder.WebSample.Pages;

public class CoverModel : PageModel
{
    public string SetTitle { get; set; } = "";
    public int CellSize { get; set; } = 40;
    public int GridSize { get; set; } = 15;
    public int PackSize { get; set; } = 0; 

    public IList<PuzzleDefinition> Puzzles { get; set; } = new List<PuzzleDefinition>();


    public async Task OnGet(string? id)
    {
        if (id != null)
        {
            var repository = new WordSearchEngine.WordSearchRepository();
            var puzzleSet = await repository.LoadSetAsync(id);
            Puzzles = new List<PuzzleDefinition>() { puzzleSet.Puzzles.First() };
            GridSize = puzzleSet.Puzzles.FirstOrDefault()?.Puzzle.Length ?? 0;
            SetTitle = puzzleSet.Title ?? string.Empty;
            PackSize = puzzleSet.Puzzles.Count;
        }

    }

}
