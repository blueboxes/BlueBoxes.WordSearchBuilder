using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.Models;


namespace BlueBoxes.WordSearchBuilder.Repositories;

/// <summary>
/// Allows the persistance of a <see cref="WordSearchSet"/> to disk.
/// </summary>
public class LocalFileRepository : IWordSearchRepository
{
    //todo: make this configurable saving to blob storage 

    public async Task<string> SaveSetAsync(WordSearchSet puzzles)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new PlacedWordJsonConverter(), new GridJsonConverter() }
        };
        var json = JsonSerializer.Serialize(puzzles, options);
        var id = BuildId();
        await File.WriteAllTextAsync(@$"D:\DevProjects\WS2022\src\UI\Saved\{id}.json", json, Encoding.UTF8);
        return id;
    }

    public async Task<WordSearchSet> LoadSetAsync(string id)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new PlacedWordJsonConverter(), new GridJsonConverter() }
        };

        var json = await File.ReadAllTextAsync($@"D:\DevProjects\WS2022\src\UI\Saved\{id}.json", Encoding.UTF8);

        return JsonSerializer.Deserialize<WordSearchSet>(json, options) ?? new WordSearchSet();
    }


    /// <remarks>Based on https://stackoverflow.com/a/15009617/33</remarks>
    private string BuildId()
    {
        long ticks = DateTime.UtcNow.Ticks;
        var hashids = new HashidsNet.Hashids("sale", 8);
        return hashids.EncodeLong(ticks);
    }
}