using BlueBoxes.WordSearchBuilder.Models;

namespace BlueBoxes.WordSearchBuilder.Repositories
{
    public interface IWordSearchRepository
    {
        Task<string> SaveSetAsync(WordSearchSet puzzles);
        Task<WordSearchSet> LoadSetAsync(string id);
    }
}