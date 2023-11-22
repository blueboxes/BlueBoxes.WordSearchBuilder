using BlueBoxes.WordSearchBuilder.Helpers;

namespace BlueBoxes.WordSearchBuilder.Models
{
    public sealed class PlacedWord
    {
        public static PlacedWord Empty = new PlacedWord();

        public string Word { get; }
        public int GridWordLength { get; }
        public Direction Direction { get; }
        public GridCell StartCell { get; }

        private PlacedWord()
        {
            Word = "";
            GridWordLength = -1;
            Direction = Direction.None;
            StartCell = new GridCell();
        }

        public PlacedWord(string word, int gridWordLength, Direction direction, GridCell initialCell)
        {
            Word = word;
            GridWordLength = gridWordLength;
            Direction = direction;
            StartCell = initialCell;
        }

        public override string ToString()
        {
            return $"{Word} Start:{StartCell.Col},{StartCell.Row} Direction: {Direction.ToColDelta()},{Direction.ToRowDelta()} Length:{GridWordLength}";
        }

    }
}
