using System.Text.Json;
using BlueBoxes.WordSearchBuilder.Helpers;

namespace BlueBoxes.WordSearchBuilder.Tests;

public class GridJsonConverterTests
{
    [Test]
    public void TestSerializeGrid()
    {
        var grid = GridExtensions.Initialize(3, 3, 'X');

        grid[0][0] = 'R';
        grid[0][1] = 'E';
        grid[0][2] = 'D';

        var options = new JsonSerializerOptions
        {
            Converters = { new GridJsonConverter() }
        };

        var json = JsonSerializer.Serialize(grid, options);

        var targetJson = $@"[
[""R"",""X"",""X""],
[""E"",""X"",""X""],
[""D"",""X"",""X""]]";

        Assert.That(json, Is.EqualTo(targetJson));
    }

    [Test]
    public void TestDeserializeGrid()
    {
        var sourceJson = $@"[[""R"",""X"",""X""],[""E"",""X"",""X""],[""D"",""X"",""X""]]";
        var options = new JsonSerializerOptions
        {
            Converters = { new GridJsonConverter() }
        };

        var result = JsonSerializer.Deserialize<char[][]>(sourceJson, options);

        var grid = GridExtensions.Initialize(3, 3, 'X');

        grid[0][0] = 'R';
        grid[0][1] = 'E';
        grid[0][2] = 'D';

        Assert.That(result, Is.EqualTo(grid));
    }
}
