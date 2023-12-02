using BlueBoxes.WordSearchBuilder;

Console.WriteLine("How many letters in size do you wish the grid to be?");
var size = Console.ReadLine();

var width = int.Parse(size ?? "10");
var height = width;

var builder = new WordSearchBuilder(width, height);

while(true)
{
    Console.WriteLine("Enter the next word (leave blank to exit):");
    var newWord = Console.ReadLine();
    if(String.IsNullOrEmpty(newWord))
        break;
    
    builder.WithWords(newWord);
}

var puzzleDef = builder.Build();

//Render the Puzzle Cols,rows
for(var row = 0; row < puzzleDef.Dimensions.Height; row++)
{
    for(var col = 0; col < puzzleDef.Dimensions.Width; col++)
    {
        Console.Write(puzzleDef.Puzzle[col][row]);
    }
    Console.WriteLine();
}

Console.WriteLine();

foreach(var word in puzzleDef.Solution)
{
    Console.WriteLine($"{word.Word} ({word.Direction} from {word.StartCell.Col}, {word.StartCell.Row})");
}