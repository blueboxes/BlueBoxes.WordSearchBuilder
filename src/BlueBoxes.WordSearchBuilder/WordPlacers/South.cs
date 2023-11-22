using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.Models;

namespace BlueBoxes.WordSearchBuilder.WordPlacers
{
    public class South : WordPlacer
    {
        public override PlacedWord TryPlaceWord(string word, char[][] grid)
        {
            if (word.Length > grid.Height())
                return PlacedWord.Empty;

            var xRange = Enumerable.Range(0, grid.Width()).ToArray();
            var yRange = Enumerable.Range(0, grid.Width() - word.Length + 1).ToArray();

            return FindWordLocation(xRange, yRange, word, grid);
        }


        public override Direction Direction { get; } = Direction.South;

    }
}
