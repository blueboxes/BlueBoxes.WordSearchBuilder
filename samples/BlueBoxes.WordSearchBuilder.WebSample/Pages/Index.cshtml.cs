using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlueBoxes.WordSearchBuilder.WebSample.Pages;

public class NewModel : PageModel
{
    public string Id { get; set; } = "";
    [BindProperty()]
    public string PuzzleTitle { get; set; } = "Colours";
    [BindProperty()]
    public int CellSize { get; set; } = 40;
    [BindProperty()]
    public int AnswerCellSize { get; set; } = 20;
    [BindProperty()]
    public int GridSize { get; set; } = 15;
    [BindProperty()]
    public string InputWords { get; set; } = $"Red,Green,Blue,Pink,Orange";
    [BindProperty()]
    public Difficulty GridLevel { get; set; } = Difficulty.Easy;

    public PuzzleDefinition? PuzzleDef { get; set; }

    public async Task OnGet(string id)
    {
        Id = id;
        if (!string.IsNullOrEmpty(Id))
        {
            var repository = new LocalFileRepository();
            PuzzleDef = await repository.LoadPuzzleAsync(Path.Join(Path.GetTempPath(), $"{Id}.json"));
            GridLevel = Enum.Parse<Difficulty>(PuzzleDef.Difficulty.ToString());
            InputWords = string.Join(",", PuzzleDef.Solution.Select(a => a.Word));
            GridSize = PuzzleDef.Dimensions.Width;
            PuzzleTitle = PuzzleDef.Title;
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var words = InputWords.Split(",");

        PuzzleLayout puzzleLayout = new EasyPuzzleLayout();
        switch (GridLevel)
        {
            case Difficulty.Easy:
                puzzleLayout = new EasyPuzzleLayout();
                break;
            case Difficulty.Medium:
                puzzleLayout = new MediumPuzzleLayout();
                break;
            case Difficulty.Hard:
                puzzleLayout = new HardPuzzleLayout();
                break;
        }

        GridSize = puzzleLayout.GridSize;

        PuzzleDef = new WordSearchBuilder(GridSize, GridSize)
            .SetDifficulty(GridLevel)
            .SetTitle(PuzzleTitle)
            .AddWords(words.Skip(1).ToArray())
            .Build();

        var puzzleId = Guid.NewGuid();
        string fileName = Path.Join(Path.GetTempPath(), $"{puzzleId}.json");
        var repository = new LocalFileRepository();
        await repository.SavePuzzleAsync(PuzzleDef,fileName);
        return Redirect($"?id={puzzleId}");
    }

}
