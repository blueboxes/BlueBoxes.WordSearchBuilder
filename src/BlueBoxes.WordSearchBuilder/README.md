# About
This package allow you to build, solve and export word search puzzles in the iPuz json format.

Words can be placed at random horizontally, vertically or diagonally, forwards or backwards. The word search can be any size, and the words to be found can be of any length.

# How to Use
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

# Additional Documentation
More examples of usage and sample projects can be found on the [Prjects GitHub](https://github.com/blueboxes/BlueBoxes.WordSearchBuilder/blob/main/README.md)