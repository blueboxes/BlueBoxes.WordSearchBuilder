using System.Text.Json;
using System.Text.Json.Serialization;
using BlueBoxes.WordSearchBuilder.WordPlacers;

namespace BlueBoxes.WordSearchBuilder.Helpers
{
    /// <summary>
    /// The format is [
    /// [col1,col2,col3 ],
    /// [col1,col2,col3],
    /// [col1,col2,col3]
    /// ]
    /// </summary>
    public class GridJsonConverter : JsonConverter<char[][]>
    {
        private record Row(int[][] position, int[][] direction, int wordLength);

        public override char[][] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var grid = new List<char[]>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.StartArray)
                {
                    var innerArray = new List<char>();
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        var s = reader.GetString() ?? "";
                        innerArray.Add(s[0]);
                    }
                    grid.Add(innerArray.ToArray());
                }
            }

            return FilpArray(grid.ToArray());
        }

        private char[][] FilpArray(char[][] source)
        {
            var flippedGrid = GridExtensions.Initialize(source.Height(), source.Width(), WordPlacer.NullChar);

            for (int col = 0; col < source.Width(); col++)
            {
                for (int row = 0; row < source.Height(); row++)
                {
                    flippedGrid[row][col] = source[col][row];
                }
            }

            return flippedGrid;
        }


        public override void Write(Utf8JsonWriter writer, char[][] value, JsonSerializerOptions options)
        {
            var flippedGrid = FilpArray(value);

            writer.WriteStartArray();

            for (var row = 0; row < flippedGrid.Length; row++)
            {
                writer.WriteRawValue(Environment.NewLine + JsonSerializer.Serialize(flippedGrid[row], new JsonSerializerOptions() { WriteIndented = false }));
            }

            writer.WriteEndArray();
        }
    }
}