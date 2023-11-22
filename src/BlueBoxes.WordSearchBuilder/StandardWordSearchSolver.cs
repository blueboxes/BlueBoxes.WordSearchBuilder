using System.Text;
using BlueBoxes.WordSearchBuilder.Formatters;
using BlueBoxes.WordSearchBuilder.Helpers;
using BlueBoxes.WordSearchBuilder.Models;

namespace BlueBoxes.WordSearchBuilder
{
    public class StandardWordSearchSolver : IWordSearchSolver
    {
        public IWordFormatter WordFormatter { get; set; } = new EnglishWordFormatter();

        public IReadOnlyCollection<PlacedWord> SeekWord(char[][] puzzleGrid, string word)
        {
            var targetWord = WordFormatter.FormatWord(word);
            if (!HasEnoughLetters(puzzleGrid, targetWord))
            {
                return new List<PlacedWord>();
            }

            var placedWords = new List<PlacedWord>();

            for (int row = 0; row < puzzleGrid.Height(); row++)
            {
                for (int col = 0; col < puzzleGrid.Width(); col++)
                {
                    if (puzzleGrid[col][row] == targetWord[0])
                    {
                        foreach (var direction in Enum.GetValues(typeof(Direction)).Cast<Direction>())
                        {
                            if (direction == Direction.None)
                                continue;

                            var placedWord = FindWordInDirection(puzzleGrid, word, targetWord, row, col, direction);

                            if (placedWord != null)
                            {
                                placedWords.Add(placedWord);
                            }
                        }
                    }
                }
            }

            return placedWords;
        }

        private PlacedWord? FindWordInDirection(char[][] puzzleGrid, string originalWord, string targetWord, int row, int col, Direction direction)
        {
            var wordLength = targetWord.Length;
            var lastRow = row + (wordLength - 1) * direction.ToRowDelta();
            var lastCol = col + (wordLength - 1) * direction.ToColDelta();

            if (lastRow < 0 || lastRow >= puzzleGrid.Height() || lastCol < 0 || lastCol >= puzzleGrid.Width())
            {
                return null;
            }

            var word = new StringBuilder(wordLength);
            for (int letterIndex = 0; letterIndex < wordLength; letterIndex++)
            {
                var currentRow = row + letterIndex * direction.ToRowDelta();
                var currentCol = col + letterIndex * direction.ToColDelta();
                var currentChar = puzzleGrid[currentCol][currentRow];

                if (currentChar != targetWord[letterIndex])
                {
                    return null;
                }

                word.Append(currentChar);
            }

            return new PlacedWord(originalWord, wordLength, direction, new GridCell(col, row));
        }

        public bool HasEnoughLetters(char[][] puzzleGrid, string targetWord)
        {
            //count number of each letters in the grid
            //count number of each letter in the word 
            //if they are less then quick exit
            var rows = puzzleGrid.Height();
            var cols = puzzleGrid.Width();

            // Count number of each letter in the grid
            var letterCounts = new Dictionary<char, int>();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var letter = puzzleGrid[col][row];
                    if (letterCounts.ContainsKey(letter))
                    {
                        letterCounts[letter]++;
                    }
                    else
                    {
                        letterCounts[letter] = 1;
                    }
                }
            }

            // Count number of each letter in the target word
            var wordCounts = new Dictionary<char, int>();
            foreach (var letter in targetWord)
            {
                if (wordCounts.ContainsKey(letter))
                {
                    wordCounts[letter]++;
                }
                else
                {
                    wordCounts[letter] = 1;
                }
            }

            // Check if all letters in targetWord exist in the grid
            foreach (var letter in wordCounts.Keys)
            {
                if (!letterCounts.ContainsKey(letter) || letterCounts[letter] < wordCounts[letter])
                {
                    // Quick exit if letter is not found in grid
                    return false;
                }
            }

            return true;
        }
    }
}
