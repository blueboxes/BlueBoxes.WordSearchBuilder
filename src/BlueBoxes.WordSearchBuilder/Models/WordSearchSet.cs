namespace BlueBoxes.WordSearchBuilder.Models;


public class WordSearchSet
{
    public string? Title { get; set; }

    public List<PuzzleDefinition> Puzzles { get; set; } = new List<PuzzleDefinition>();
}