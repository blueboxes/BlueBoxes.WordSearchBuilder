using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.Models;

namespace BlueBoxes.WordSearchBuilder.WordPlacers
{
    public class NorthEast : WordPlacer
    {

        public override PlacedWord TryPlaceWord(string word, char[][] grid)
        {
            if (word.Length > grid.Width() || word.Length > grid.Height())
                return PlacedWord.Empty;

            var xRange = Enumerable.Range(0, grid.Width() - word.Length + 1).ToArray();
            var yRange = Enumerable.Range(word.Length - 1, grid.Height() - word.Length + 1).ToArray();

            return FindWordLocation(xRange, yRange, word, grid);
        }

        public override Direction Direction { get; } = Direction.NorthEast;


    }
}
