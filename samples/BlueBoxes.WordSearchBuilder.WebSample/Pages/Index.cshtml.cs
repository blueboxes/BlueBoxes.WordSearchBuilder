using Microsoft.AspNetCore.Mvc.RazorPages;
using BlueBoxes.WordSearchBuilder.WebSample.Models;

namespace BlueBoxes.WordSearchBuilder.WebSample.Pages;

public class IndexModel : PageModel
{

    public IList<PuzzleMenuItem> Puzzles { get; set; } = new List<PuzzleMenuItem>();

    public async Task OnGet()
    {
        //todo:load list of puzzles
        var files = Directory.GetFiles("D:\\DevProjects\\WS2022\\src\\UI\\Saved\\");
        foreach (var file in files)
        {
            var f = new FileInfo(file);
            var repository = new WordSearchEngine.WordSearchRepository();
            var id = f.Name.Substring(0, f.Name.IndexOf("."));
            var puzzleSet = await repository.LoadSetAsync(id);
            Puzzles.Add(new PuzzleMenuItem() { Title = file, Id = id });
        }

        @ViewData["Title"] = "Word Search Puzzle Generator";
    }

}
