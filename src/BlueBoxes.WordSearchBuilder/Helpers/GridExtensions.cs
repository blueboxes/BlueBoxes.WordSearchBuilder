namespace BlueBoxes.WordSearchBuilder.Helpers
{
    public static class GridExtensions
    {
        //Grid is structured as [col][row]
        public static int Width(this char[][] grid)
        {
            return grid.Length;
        }

        public static int Height(this char[][] grid)
        {
            return grid[0].Length;
        }

        public static char[][] Initialize(int width, int height, char emptyChar)
        {
            var grid = new char[width][];

            for (int col = 0; col < width; col++)
            {
                grid[col] = new char[height];

                for (int row = 0; row < height; row++)
                {
                    grid[col][row] = emptyChar;
                }
            }

            return grid;
        }

        public static char[][] DeepClone(this char[][] source)
        {
            if (source == null)
            {
                return new char[0][];
            }

            char[][] clone = new char[source.Length][];
            for (int i = 0; i < source.Length; i++)
            {
                clone[i] = new char[source[i].Length];
                for (int j = 0; j < source[i].Length; j++)
                {
                    clone[i][j] = source[i][j];
                }
            }

            return clone;
        }

    }
}
