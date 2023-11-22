namespace BlueBoxes.WordSearchBuilder.Helpers;

static class SortingExtensions
{
    private readonly static Random rnd = new Random();

    public static IList<T> Shuffle<T>(this IList<T> items)
    {
        return items.OrderBy(x => rnd.Next(0, 10)).ToList();
    }

    /// <summary>
    /// Sort words that are most likly to allow other words to be placed
    /// Longer words score more as harder to place and so do words with more common letters
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public static IList<string> SortByComplexity(this IList<string> items)
    {
        return items.OrderByDescending(word => word.Sum(ScoreLetter)).ToList();
    }

    private static int ScoreLetter(char letter)
    {
        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        var weights = new int[] { 8, 2, 3, 4, 13, 2, 2, 6, 7, 1, 1, 4, 2, 7, 8, 2, 1, 6, 6, 9, 3, 1, 2, 1, 2, 1 };

        letter = char.ToUpper(letter);
        int index = Array.IndexOf(letters, letter);

        return index >= 0 && index < weights.Length ? weights[index] : 0;

    }
}
