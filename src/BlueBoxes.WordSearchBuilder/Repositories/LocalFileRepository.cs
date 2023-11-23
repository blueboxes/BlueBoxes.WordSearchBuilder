using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.Models;


namespace BlueBoxes.WordSearchBuilder.Repositories;

/// <summary>
/// Allows the persistance of a <see cref="PuzzleDefinition"/> to disk.
/// </summary>
public class LocalFileRepository : IWordSearchFileRepository
{
    public async Task SavePuzzleAsync(PuzzleDefinition puzzle, string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new PlacedWordJsonConverter(), new GridJsonConverter() }
        };
        var json = JsonSerializer.Serialize(puzzle, options);
        await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);
    }

    public async Task<PuzzleDefinition> LoadPuzzleAsync(string filePath)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new PlacedWordJsonConverter(), new GridJsonConverter() }
        };

        var json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);

        return JsonSerializer.Deserialize<PuzzleDefinition>(json, options) ?? new PuzzleDefinition();
    }

 
}