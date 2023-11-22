namespace BlueBoxes.WordSearchBuilder.Models
{
    public class PuzzleDefinition
    {
        public string? Copyright { get; set; }
        public string? Publisher { get; set; }
        public string? Uniqueid { get; set; }
        public DateTime? Date { get; set; }
        public string? Url { get; set; }
        public string? Difficulty { get; set; }
        public string Origin => "WordSearchEngine2022";
        public string? Title { get; set; }
        public string? Notes { get; set; }
        public string Version => "http://ipuz.org/v1";
        public string[] Kind => new string[] { "http://ipuz.org/wordsearch#1" };
        public GridDimension Dimensions { get; set; } = GridDimension.Empty;
        public char[][] Puzzle { get; set; } = new char[0][];
        public List<PlacedWord> Solution { get; set; } = Array.Empty<PlacedWord>().ToList();
    }
}
