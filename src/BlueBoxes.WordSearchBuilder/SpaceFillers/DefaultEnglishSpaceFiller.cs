using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.WordPlacers;

namespace BlueBoxes.WordSearchBuilder.SpaceFillers
{
    /// <summary>
    /// Fills the grid with letters where it finds empty cells
    /// </summary>
    public class DefaultEnglishSpaceFiller : ISpaceFiller
    {
        public virtual char[][] FillSpacesInGrid(char[][] grid)
        {
            for (int col = 0; col < grid.Width(); col++)
            {
                for (int row = 0; row < grid.Height(); row++)
                {
                    if (grid[col][row] == WordPlacer.NullChar)
                    {
                        grid[col][row] = GetWeightedRandomLetter();
                    }
                }
            }

            return grid;
        }

        /// <summary>
        /// Gets random letter from the alphabet weighted by the frequency of the letter in the English language
        /// https://en.wikipedia.org/wiki/Letter_frequency
        /// </summary>
        /// <returns>Random Letter</returns>
        protected char GetWeightedRandomLetter()
        {
            var rnd = new Random();
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var weights = new int[] { 8, 2, 3, 4, 13, 2, 2, 6, 7, 1, 1, 4, 2, 7, 8, 2, 1, 6, 6, 9, 3, 1, 2, 1, 2, 1 };
            var total = weights.Sum();

            var r = rnd.Next(0, total);
            var sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i];
                if (r < sum)
                {
                    return letters[i];
                }
            }
            return letters[0];
        }

    }
}
