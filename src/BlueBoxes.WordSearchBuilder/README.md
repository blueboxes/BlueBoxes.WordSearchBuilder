# About
This package allow you to build, solve and export wordsearch puzzles in the iPuz json format.

Words are be placed at random horizontally, vertically or diagonally both forwards and backwards. The wordsearch can be any size, and the words to be found can be of any length.

# How to Use
The wordsearch builder allows adding words to the wordsearch grid, upon calling `Build` it fills the spaces with random letters.

```csharp
var puzzleDef = new WordSearchBuilder(10, 10)
    .AddWords("Apple", "Orange", "Grape")
    .Build();
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

# Additional Documentation
More examples of usage and sample projects can be found on the [Projects GitHub](https://github.com/blueboxes/BlueBoxes.WordSearchBuilder/blob/main/README.md)