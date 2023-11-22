using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.WordPlacers;

namespace BlueBoxes.WordSearchBuilder.Tests.WordPlacers;

public class NorthEastTests
{
    [Test]
    public void WhenPlaceWordNorthEastSuccessfully()
    {
        var placer = new NorthEast();
        var wordSearch = new WordSearchBuilder(4, 4);
        var result = placer.TryPlaceWord("TEST", wordSearch.Grid);
        result.Should().NotBeNull();
        result.Should().NotBe(PlacedWord.Empty);

        result.StartCell.Col.Should().Be(0);
        result.StartCell.Row.Should().Be(3);

        wordSearch.Grid[result.StartCell.Col][result.StartCell.Row].Should().Be('T');
        wordSearch.Grid[result.StartCell.Col + 1][result.StartCell.Row - 1].Should().Be('E');
        wordSearch.Grid[result.StartCell.Col + 2][result.StartCell.Row - 2].Should().Be('S');
        wordSearch.Grid[result.StartCell.Col + 3][result.StartCell.Row - 3].Should().Be('T');
    }

    [Test]
    public void WhenPlaceWordInPopulatedGridSuccessfully()
    {
        var placer = new NorthEast();
        var wordSearch = new WordSearchBuilder(4, 4);
        wordSearch.Grid[1][0] = 'P';
        wordSearch.Grid[1][1] = 'A';
        wordSearch.Grid[1][2] = 'E';
        wordSearch.Grid[1][3] = 'L';
        var result = placer.TryPlaceWord("TEST", wordSearch.Grid);
        result.Should().NotBeNull();
        result.Should().NotBe(PlacedWord.Empty);

        result.StartCell.Col.Should().Be(0);
        result.StartCell.Row.Should().Be(3);

        wordSearch.Grid[0][3].Should().Be('T');
        wordSearch.Grid[1][2].Should().Be('E');
        wordSearch.Grid[2][1].Should().Be('S');
        wordSearch.Grid[3][0].Should().Be('T');
    }

    [Test]
    public void WhenPlaceWordInSmallGridFail()
    {
        var placer = new NorthEast();
        var wordSearch = new WordSearchBuilder(3, 3);
        var result = placer.TryPlaceWord("TEST", wordSearch.Grid);
        result.Should().Be(PlacedWord.Empty);
    }

    [Test]
    public void WhenPlaceWordInPopulatedGridFail()
    {
        var placer = new NorthEast();
        var wordSearch = new WordSearchBuilder(4, 4);
        wordSearch.Grid[0][0] = 'T';
        wordSearch.Grid[0][1] = 'E';
        wordSearch.Grid[0][2] = 'S';
        wordSearch.Grid[0][3] = 'T';
        var result = placer.TryPlaceWord("LOAX", wordSearch.Grid);
        result.Should().Be(PlacedWord.Empty);
    }
}