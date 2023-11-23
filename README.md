# BlueBoxes.WordSearchBuilder

This package is a .Net 8 library for creating, solving and exporting word searches in json [iPuz format](https://ipuz.readthedocs.io/en/latest/reading.html#validation-for-wordsearch-puzzles).

Words can be placed at random horizontally, vertically or diagonally, forwards or backwards. The word search can be any size, and the words to be found can be of any length.

Snaking Word Search puzzles where the words can change direction are not supported directly however could easily be added.

## Getting started

The builder allows you to add words to a grid, upon calling `Build` it fills the spaces with random letters.

```csharp
var builder = new WordSearchBuilder(10, 10);    
builder.AddWords("Apple", "Orange", "Grape");
var puzzleDef = builder.Build();
```

To render the results you can loop over the grid.

```csharp
for(var row = 0; row < puzzleDef.Dimensions.Height; row++)
{
    for(var col = 0; col < puzzleDef.Dimensions.Width; col++)
    {
        Console.Write(puzzleDef.Puzzle[col][row]);
    }
    Console.WriteLine();
}

```

The puzzle definition also includes the solution. Items that could not be placed are not included in the solution.

```csharp
foreach(var word in puzzleDef.Solution)
{
    Console.WriteLine($"{word.Word} ({word.Direction} from {word.StartCell.Col}, {word.StartCell.Row})");
}
```

### Prerequisites

This package requires the following dependencies:

- .net 8.0

## Usage

### Difficulty level
By default difficulty level it is set to Easy mode. The difficulty level can be set in the constructor or through the property `DifficultyLevel`.

The are 3 levels:

1. `DifficultyLevel.Easy` - This places words left to right and top to bottom with a few diagrams.
2. `DifficultyLevel.Medium` - Sets an equal amount of left to right, top to bottom and diagonal words with a few reversed.
3. `DifficultyLevel.Hard` - Gives an equal waiting to all directions of words.

This sets the group of word placers that are randomly picked from when placing words. You can set your own to override this using the `SetWordPlacers` method.

### Removing Bad Words
Example Coming Soon...

### Solving a word search
Example Coming Soon...

### Saving and Loading a word search
You can save and load a word search by serializing the Puzzle Definition. To ensure it is formatted correctly for the iPuz format converters are provided. 

The easiest way to save and load the puzzle is to use the repository class. At the moment there is only a local file repository however this can easily be extended.

```csharp
var repo = new LocalFileRepository();
await repo.SavePuzzleAsync(puzzleDefinition, @"c:\files\puzzle.ipuz");
puzzleDefinition = await repo.LoadPuzzleAsync(@"c:\files\puzzle.ipuz");
```

Further samples can be found in the samples folder of this repository.

## Additional documentation

* WordSearches in [iPuz format](https://ipuz.readthedocs.io/en/latest/reading.html#validation-for-wordsearch-puzzles)

## Future Plans
This code is based on .Net 8 rather than .Net Standard 2.0 as it was part of a larger personal project before making in to a shareable library. This means that it will not work on .Net Framework or older version of Core. I would be open to adding support for .Net Standard 2.0 in the future.

Currently it only supports English character sets although has been built with others in mind. I would like to add support for other languages in the future and would welcome contributions.