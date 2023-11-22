namespace BlueBoxes.WordSearchBuilder.Formatters
{
    /// <summary>
    /// Formats the input word to fit into the grid removing charactors that are no accepted
    /// </summary>
    public interface IWordFormatter
    {
        string FormatWord(string inputWord);
    }
}