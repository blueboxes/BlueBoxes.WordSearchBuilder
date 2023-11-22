namespace BlueBoxes.WordSearchBuilder.Models
{
    public class GridDimension
    {
        public static GridDimension Empty = new GridDimension();
        public int Height { get; set; } = 0;
        public int Width { get; set; } = 0;
    }
}