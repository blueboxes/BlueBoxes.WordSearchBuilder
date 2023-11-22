namespace BlueBoxes.WordSearchBuilder.Models
{
    /// <summary>
    /// Co-ordinates of a Grid Cell to be found inside the grid
    /// </summary>
    public struct GridCell
    {
        public int Col { get; private set; }
        public int Row { get; private set; }
        public static GridCell Empty = new GridCell(-1, -1);

        public GridCell(int col, int row) : this()
        {
            Col = col;
            Row = row;
        }
    }
}
