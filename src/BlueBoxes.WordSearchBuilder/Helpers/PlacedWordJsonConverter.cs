using System.Text.Json;
using System.Text.Json.Serialization;
using BlueBoxes.WordSearchBuilder.Models;

namespace BlueBoxes.WordSearchBuilder.Helpers
{
    /// <summary>
    /// The format is [[X - Col, Y - Row],[Direction Col,D Direction Row],WordLength] 
    /// </summary>
    public class PlacedWordJsonConverter : JsonConverter<List<PlacedWord>>
    {
        private record Row(int[][] position, int[][] direction, int wordLength);

        public override List<PlacedWord> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var words = new List<PlacedWord>();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var currentWord = reader.GetString() ?? "";

                    reader.Read();//moves to the outer array

                    var (startCol, startRow) = GetArray(ref reader);
                    var (directionX, directionY) = GetArray(ref reader);

                    reader.Read();//reads final item 
                    var wordLength = reader.GetInt32();

                    reader.Read();//reads end of outer array

                    var direction = DirectionLookUp.GetDirection(directionX, directionY);
                    if (direction == Direction.None)
                        throw new NullReferenceException("direction error");

                    words.Add(new PlacedWord(currentWord, wordLength, direction, new GridCell(startCol, startRow)));
                }
            }

            return words;
        }

        private (int x, int y) GetArray(ref Utf8JsonReader reader)
        {
            //readers should be at the start of the array
            //in the format [0,0]

            reader.Read();//start array

            reader.Read();
            var x = reader.GetInt32();

            reader.Read();
            var y = reader.GetInt32();

            reader.Read(); //end array

            return (x, y);
        }

        public override void Write(Utf8JsonWriter writer, List<PlacedWord> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var placedWord in value)
            {
                writer.WritePropertyName(placedWord.Word);
                writer.WriteRawValue($"[[{placedWord.StartCell.Col},{placedWord.StartCell.Row}],[{placedWord.Direction.ToColDelta()},{placedWord.Direction.ToRowDelta()}],{placedWord.GridWordLength}]");
            }

            writer.WriteEndObject();
        }
    }
}