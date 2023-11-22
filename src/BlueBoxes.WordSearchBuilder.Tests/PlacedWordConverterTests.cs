using System.Text.Json;
using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.WordPlacers;

namespace BlueBoxes.WordSearchBuilder.Tests;

public class PlacedWordConverterTests
{
    [Test]
    public void TestSerializeDiagonalLeftDown()
    {
        var dld = new SouthEast();
        List<PlacedWord> word = new List<PlacedWord> { new PlacedWord("hello world", 10, dld.Direction, new GridCell(0, 0)) };

        var options = new JsonSerializerOptions
        {
            Converters = { new PlacedWordJsonConverter() }
        };

        var json = JsonSerializer.Serialize(word, options);

        Assert.That(json, Is.EqualTo("{\"hello world\":[[0,0],[1,1],10]}"));
    }

    [Test]
    public void TestSerializeNorthEast()
    {
        var dld = new NorthEast();
        List<PlacedWord> word = new List<PlacedWord> { new PlacedWord("hello world", 10, dld.Direction, new GridCell(0, 10)) };

        var options = new JsonSerializerOptions
        {
            Converters = { new PlacedWordJsonConverter() }
        };

        var json = JsonSerializer.Serialize(word, options);

        Assert.That(json, Is.EqualTo("{\"hello world\":[[0,10],[1,-1],10]}"));
    }

    [Test]
    public void TestSerializeSouth()
    {
        var dld = new South();
        List<PlacedWord> word = new List<PlacedWord> { new PlacedWord("hello world", 10, dld.Direction, new GridCell(0, 0)) };

        var options = new JsonSerializerOptions
        {
            Converters = { new PlacedWordJsonConverter() }
        };

        var json = JsonSerializer.Serialize(word, options);

        Assert.That(json, Is.EqualTo("{\"hello world\":[[0,0],[0,1],10]}"));
    }

    [Test]
    public void TestSerializeNorth()
    {
        var dld = new Reversed(new South());
        List<PlacedWord> word = new List<PlacedWord> { new PlacedWord("hello world", 10, dld.Direction, new GridCell(0, 0)) };

        var options = new JsonSerializerOptions
        {
            Converters = { new PlacedWordJsonConverter() }
        };

        var json = JsonSerializer.Serialize(word, options);

        Assert.That(json, Is.EqualTo("{\"hello world\":[[0,0],[0,-1],10]}"));
    }

    [Test]
    public void TestDeserializeDiagonalLeftDown()
    {
        var dld = new SouthEast();

        var options = new JsonSerializerOptions
        {
            Converters = { new PlacedWordJsonConverter() }
        };

        var json = "{\"hello world\":[[2,2],[1,1],10]}";
        var word = JsonSerializer.Deserialize<List<PlacedWord>>(json, options);

        var targetWord = new PlacedWord("hello world", 10, dld.Direction, new GridCell(2, 2));

        word?.First().Should().BeEquivalentTo(targetWord);
    }

    [Test]
    public void TestDeSerializeDiagonalLeftUp()
    {
        var dld = new NorthEast();

        var options = new JsonSerializerOptions
        {
            Converters = { new PlacedWordJsonConverter() }
        };

        var json = "{\"hello world\":[[0,10],[1,-1],10]}";
        var word = JsonSerializer.Deserialize<List<PlacedWord>>(json, options);
        var targetWord = new PlacedWord("hello world", 10, dld.Direction, new GridCell(0, 10));

        word?.First().Should().BeEquivalentTo(targetWord);
    }

    [Test]
    public void TestDeSerializeHorizontal()
    {
        var dld = new East();


        var options = new JsonSerializerOptions
        {
            Converters = { new PlacedWordJsonConverter() }
        };

        var json = "{\"hello world\":[[0,0],[1,0],10]}";
        var word = JsonSerializer.Deserialize<List<PlacedWord>>(json, options);
        var targetWord = new PlacedWord("hello world", 10, dld.Direction, new GridCell(0, 0));

        word?.First().Should().BeEquivalentTo(targetWord);
    }

    [Test]
    public void TestDeSerializeVertical()
    {
        var dld = new South();

        var options = new JsonSerializerOptions
        {
            Converters = { new PlacedWordJsonConverter() }
        };

        var json = "{\"hello world\":[[0,0],[0,1],10]}";
        var word = JsonSerializer.Deserialize<List<PlacedWord>>(json, options);
        var targetWord = new PlacedWord("hello world", 10, dld.Direction, new GridCell(0, 0));

        word?.First().Should().BeEquivalentTo(targetWord);
    }
}
