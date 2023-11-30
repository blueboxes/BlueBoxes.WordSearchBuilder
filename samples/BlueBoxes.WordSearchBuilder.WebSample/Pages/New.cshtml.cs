using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;
using WordSearchEngine.Models;
using static WordSearchEngine.WordSearchBuilder;

namespace BlueBoxes.WordSearchBuilder.WebSample.Pages;

public class NewModel : PageModel
{
    public string PuzzleSetId { get; set; } = "";
    [BindProperty()]
    public string SetTitle { get; set; } = "";
    [BindProperty()]
    public int CellSize { get; set; } = 40;
    [BindProperty()]
    public int AnswerCellSize { get; set; } = 20;
    [BindProperty()]
    public int GridSize { get; set; } = 15;
    [BindProperty()]
    public string InputWords { get; set; } = $"Colors,Red,Green,Blue,Pink,Orange{Environment.NewLine}Colors 2,Red,Green,Blue,Pink,Orange{Environment.NewLine}Colors 3,Red,Green,Blue,Pink,Orange";
    [BindProperty()]
    public Difficulty GridLevel { get; set; } = Difficulty.Easy;

    public IList<PuzzleDefinition> Puzzles { get; set; } = new List<PuzzleDefinition>();

    public async Task OnGet(string? id)
    {
        if (id != null)
        {
            PuzzleSetId = id;

            var repository = new WordSearchEngine.WordSearchRepository();
            var puzzleSet = await repository.LoadSetAsync(id);
            Puzzles = puzzleSet.Puzzles;
            GridSize = puzzleSet.Puzzles.FirstOrDefault()?.Puzzle.Length ?? 0;
            InputWords = string.Join(Environment.NewLine, puzzleSet.Puzzles.Select(a => $"{a.Title},{string.Join(",", a.Solution.Select(b => b.Word))}"));
            GridLevel = Enum.TryParse(puzzleSet.Puzzles.FirstOrDefault()?.Difficulty, out Difficulty difficulty) ? difficulty : Difficulty.Easy;
            SetTitle = puzzleSet.Title ?? string.Empty;
        }

        @ViewData["Title"] = "Word Search Puzzle Generator";
    }

    public async Task<IActionResult> OnPost()
    {

        foreach (var puzzleRow in InputWords?.Split(Environment.NewLine) ?? Array.Empty<string>())
        {
            var words = puzzleRow.Split(",");

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

            var ws = new WordSearchEngine.WordSearchBuilder(GridSize, GridSize, words[0], GridLevel);
            ws.AddWords(words.Skip(1).ToArray());
            Puzzles.Add(ws.Build());
        }

        var puzzleSet = new WordSearchEngine.WordSearchSet();
        puzzleSet.Title = SetTitle;
        puzzleSet.Puzzles.AddRange(Puzzles);

        var repository = new WordSearchEngine.WordSearchRepository();
        var id = await repository.SaveSetAsync(puzzleSet);
        return Redirect($"?id={id}");
    }

}
