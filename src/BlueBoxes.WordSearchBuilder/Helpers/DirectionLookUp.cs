using BlueBoxes.WordSearchBuilder.Models;

namespace BlueBoxes.WordSearchBuilder.Helpers
{
    public static class DirectionLookUp
    {
        public static Direction GetDirection(int col, int row)
        {
            //for each item in Direction Enum call RowDelta and ColdDelte to work out the Direction from the inputs 
            foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
            {
                if (direction == Direction.None) continue;
                if (row == direction.ToRowDelta() &&
                   col == direction.ToColDelta())
                    return direction;
            }

            return Direction.None;
        }

        public static int ToRowDelta(this Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                case Direction.NorthEast:
                case Direction.NorthWest:
                    return -1;
                case Direction.South:
                case Direction.SouthEast:
                case Direction.SouthWest:
                    return 1;
                default:
                    return 0;
            }
        }

        public static int ToColDelta(this Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                case Direction.NorthEast:
                case Direction.SouthEast:
                    return 1;
                case Direction.West:
                case Direction.NorthWest:
                case Direction.SouthWest:
                    return -1;
                default:
                    return 0;
            }
        }

    }
}
