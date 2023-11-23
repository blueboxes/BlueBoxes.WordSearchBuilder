using BlueBoxes.WordSearchBuilder.Models;

namespace BlueBoxes.WordSearchBuilder.Repositories
{
    public interface IWordSearchFileRepository
    {
        Task SavePuzzleAsync(PuzzleDefinition puzzle, string filePath);
        Task<PuzzleDefinition> LoadPuzzleAsync(string filePath);
    }
}