
namespace BlueBoxes.WordSearchBuilder.WordPlacers
{
    internal class PlacerSets
    {
        //Easy = Vertical, Horizontal few Diagonal Left Down, Diagonal Left Up
        //Medium = Vertical, Horizontal, Diagonal Left Down, Diagonal Left Up
        //Hard = Vertical, Horizontal, Diagonal Left Down, Diagonal Left Up, Reversed Vertical, Reversed Horizontal, Reversed Diagonal Left Down, Reversed Diagonal Left Up

        private static readonly List<WordPlacer> PlacersEasy = new List<WordPlacer>{
                                                new SouthEast(),
                                                new NorthEast(),
                                                new South(),
                                                new East(),
                                                new South(),
                                                new East(),
                                                new South(),
                                                new East(),
                                                new South(),
                                                new East(),
                                                new South(),
                                                new East(),
                                                new South(),
                                                new East(),
                                                };

        private static readonly List<WordPlacer> PlacersMedium = new List<WordPlacer>{
                                                new SouthEast(),
                                                new NorthEast(),
                                                new South(),
                                                new East(),
                                                new SouthEast(),
                                                new NorthEast(),
                                                new South(),
                                                new East(),
                                                new SouthEast(),
                                                new NorthEast(),
                                                new South(),
                                                new East(),
                                                new SouthEast(),
                                                new NorthEast(),
                                                new South(),
                                                new East(),
                                                new Reversed(new SouthEast()),
                                                new Reversed(new NorthEast()),
                                                new Reversed(new South()),
                                                new Reversed(new East()),
                                                };

        private readonly static List<WordPlacer> PlacersHard = new List<WordPlacer>{
                                                new SouthEast(),
                                                new NorthEast(),
                                                new South(),
                                                new East(),
                                                new Reversed(new SouthEast()),
                                                new Reversed(new NorthEast()),
                                                new Reversed(new South()),
                                                new Reversed(new East()),
                                                };

        internal static List<WordPlacer> GetSet(WordSearchBuilder.Difficulty complexityLevel)
        {
            switch (complexityLevel)
            {
                case WordSearchBuilder.Difficulty.Easy:
                    return PlacersEasy;
                case WordSearchBuilder.Difficulty.Medium:
                    return PlacersMedium;
                case WordSearchBuilder.Difficulty.Hard:
                    return PlacersHard;
                default:
                    return PlacersHard;
            }
        }
    }
}
