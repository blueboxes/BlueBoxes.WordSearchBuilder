using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlueBoxes.WordSearchBuilder.WebSample.Pages;

public class WebModel : PageModel
{
    public string SetTitle { get; set; } = "";
    public Difficulty GridLevel { get; set; } = Difficulty.Easy;
    public PuzzleDefinition? PuzzleDef { get; set; }
    public PuzzleLayout PuzzleLayout { get; set; } = new EasyPuzzleLayout();
  
    public async Task OnGet(string? id)
    {
        if (id != null)
        {
            var repository = new LocalFileRepository();
            PuzzleDef = await repository.LoadPuzzleAsync(Path.Join(Path.GetTempPath(), $"{id}.json"));
            //InputWords = Puzzle.Solution(a => $"{a.Title},{string.Join(",", a.Solution.Select(b => b.Word))}");
            GridLevel = Enum.TryParse(PuzzleDef.Difficulty, out Difficulty difficulty) ? difficulty : Difficulty.Easy;
            SetTitle = PuzzleDef.Title ?? string.Empty;
            
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
