using System.Text.RegularExpressions;

namespace BlueBoxes.WordSearchBuilder.Formatters;
public class EnglishWordFormatter : IWordFormatter
{
    /// <summary>
    /// Formats the input word to fit into the grid removing spaces, hyphens etc.
    /// </summary>
    /// <param name="inputWord"></param>
    /// <returns></returns>
    public string FormatWord(string inputWord)
    {
        return Regex.Replace(inputWord.ToUpperInvariant(), "[^A-Z0-9]", "");
    }
}
