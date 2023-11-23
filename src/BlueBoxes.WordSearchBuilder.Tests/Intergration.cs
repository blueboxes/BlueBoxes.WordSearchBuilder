using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.Models;
using BlueBoxes.WordSearchBuilder.Repositories;

namespace BlueBoxes.WordSearchBuilder.Tests
{
    internal class IntergrationTests
    {

        [Test]
        public async Task SaveAndLoad()
        {
            var ws = new WordSearchBuilder(10, 10, "Title");
            ws.AddWords("word1", "word2", "word3");

            var pd = ws.Build();
             
            var repo = new LocalFileRepository();
            var fileName = Path.GetTempFileName();
            await repo.SavePuzzleAsync(pd, fileName);
            var loadedPuzzleSet = await repo.LoadPuzzleAsync(fileName);

            loadedPuzzleSet.Should().BeEquivalentTo(pd);
        }

        [Test]
        public void CreateValidSolutions()
        {
            var ws = new WordSearchBuilder(100, 100, "Title");
            ws.AddWords("word1", "word2", "word3");

            var pd = ws.Build();

            foreach (PlacedWord solution in pd.Solution)
            {

                for (int letterIndex = 0; letterIndex < solution.GridWordLength; letterIndex++)
                {
                    var letter = pd.Puzzle[solution.StartCell.Col + solution.Direction.ToColDelta() * letterIndex][solution.StartCell.Row + solution.Direction.ToRowDelta() * letterIndex];
                    letter.Should().BeEquivalentTo(solution.Word.ToUpperInvariant()[letterIndex]);
                }
            }
        }

        [Test]
        public void CreateAndSolve()
        {
            var ws = new WordSearchBuilder(10, 10, "Title");
            ws.AddWords("word1", "word2", "word3");

            var pd = ws.Build();

            var sol = new StandardWordSearchSolver();

            foreach (var word in pd.Solution)
            {
                var result = sol.SeekWord(pd.Puzzle, word.Word);
                result.First().Should().BeEquivalentTo(word);
            }

        }
    }
}
