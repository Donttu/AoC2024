using System;
using System.IO;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose a part to execute:");
            Console.WriteLine("1. Part 1");
            Console.WriteLine("2. Part 2");

            string choice = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter the input (separated by newline):");

            string input = string.Empty;
            string line;
            while ((line = Console.ReadLine() ?? string.Empty) != string.Empty)
            {
                input += line + Environment.NewLine;
            }

            string[] lines = input.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            switch (choice)
            {
                case "1":
                    Part1.Execute(lines);
                    break;
                case "2":
                    Part2.Execute(lines);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose a valid part.");
                    break;
            }
        }
    }

    public class Part1
    {
        /// <summary>
        /// Executes part 1 of the day's puzzle.
        /// Reads the input, converts it into a 2D matrix, and counts the number of times
        /// the string "XMAS" appears horizontally, vertically or diagonally in the matrix.
        /// Prints the count.
        /// </summary>
        /// <param name="lines">Array of strings, each containing a line of input.</param>
        public static void Execute(string[] lines)
        {
            char[,] matrix = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                }
            }

            string target = "XMAS";
            int count = CountCombinations(matrix, target);

            Console.WriteLine($"Target combination '{target}' found {count} times");
            Console.WriteLine("Part 1 executed.");
        }

        /// <summary>
        /// Counts the number of times a given string target appears in a given 2D matrix horizontally, vertically or diagonally.
        /// </summary>
        /// <param name="matrix">The 2D matrix to search in.</param>
        /// <param name="target">The string to search for.</param>
        /// <returns>The count of the number of times the target appears in the matrix.</returns>
        public static int CountCombinations(char[,] matrix, string target)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int count = 0;

            for (int i = 0; i < rows; i++) // horizontal
            {
                for (int j = 0; j <= cols - 4; j++)
                {
                    if (j + 3 < cols)
                    {
                        string combo = matrix[i, j].ToString() + matrix[i, j + 1].ToString() + matrix[i, j + 2].ToString() + matrix[i, j + 3].ToString();
                        if (combo == target || combo == new string(target.Reverse().ToArray()))
                        {
                            count++;
                        }
                    }
                }
            }

            for (int i = 0; i <= rows - 4; i++) // vertical
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i + 3 < rows)
                    {
                        string combo = matrix[i, j].ToString() + matrix[i + 1, j].ToString() + matrix[i + 2, j].ToString() + matrix[i + 3, j].ToString();
                        if (combo == target || combo == new string(target.Reverse().ToArray()))
                        {
                            count++;
                        }
                    }
                }
            }

            for (int i = 0; i <= rows - 4; i++) // diagonal
            {
                for (int j = 0; j <= cols - 4; j++)
                {
                    if (i + 3 < rows && j + 3 < cols)
                    {
                        string combo = matrix[i, j].ToString() + matrix[i + 1, j + 1].ToString() + matrix[i + 2, j + 2].ToString() + matrix[i + 3, j + 3].ToString();
                        if (combo == target || combo == new string(target.Reverse().ToArray()))
                        {
                            count++;
                        }
                    }
                }
            }

            for (int i = 3; i < rows; i++) // diagonal
            {
                for (int j = 0; j <= cols - 4; j++)
                {
                    if (i - 3 >= 0 && j + 3 < cols)
                    {
                        string combo = matrix[i, j].ToString() + matrix[i - 1, j + 1].ToString() + matrix[i - 2, j + 2].ToString() + matrix[i - 3, j + 3].ToString();
                        if (combo == target || combo == new string(target.Reverse().ToArray()))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }

    public class Part2
    {
        /// <summary>
        /// Executes part 2 of the day's puzzle.
        /// Counts the number of times the target combination appears in the shape of an X in the given matrix.
        /// Prints the count.
        /// </summary>
        /// <param name="lines">An array of strings, each containing a line of input.</param>
        public static void Execute(string[] lines)
        {
            char[,] matrix = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                }
            }

            string target = "MAS";

            int count = CountCombinations(matrix, target);

            Console.WriteLine($"Target combination '{target}' found {count} times in the shape of an X");

            Console.WriteLine("Part 2 executed.");
        }

        /// <summary>
        /// Counts the number of times the target combination appears in the shape of an X in the given matrix.
        /// </summary>
        /// <param name="matrix">The matrix to search for the target combination.</param>
        /// <param name="target">The combination to search for.</param>
        /// <returns>The count of the target combination in the given matrix.</returns>
        public static int CountCombinations(char[,] matrix, string target)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int count = 0;

            for (int i = 1; i < rows - 1; i++)
            {
                for (int j = 1; j < cols - 1; j++)
                {
                    if (matrix[i, j] == 'A')
                    {
                        if ((matrix[i - 1, j - 1] == 'M' && matrix[i + 1, j + 1] == 'S' && ((matrix[i - 1, j + 1] == 'S' && matrix[i + 1, j - 1] == 'M') || (matrix[i + 1, j - 1] == 'S' && matrix[i - 1, j + 1] == 'M')))) // M is top left, S is bottom right
                        {
                            count++;
                        }
                        else if ((matrix[i - 1, j + 1] == 'M' && matrix[i + 1, j - 1] == 'S' && ((matrix[i - 1, j - 1] == 'S' && matrix[i + 1, j + 1] == 'M') || (matrix[i + 1, j + 1] == 'S' && matrix[i - 1, j - 1] == 'M')))) // M is top right, S is bottom left
                        {
                            count++;
                        }
                        else if ((matrix[i + 1, j - 1] == 'M' && matrix[i - 1, j + 1] == 'S' && ((matrix[i - 1, j - 1] == 'S' && matrix[i + 1, j + 1] == 'M') || (matrix[i + 1, j + 1] == 'S' && matrix[i - 1, j - 1] == 'M')))) // M is bottom left, S is top right
                        {
                            count++;
                        }
                        else if ((matrix[i + 1, j + 1] == 'M' && matrix[i - 1, j - 1] == 'S' && ((matrix[i + 1, j - 1] == 'S' && matrix[i - 1, j + 1] == 'M') || (matrix[i - 1, j + 1] == 'S' && matrix[i + 1, j - 1] == 'M')))) // M is bottom right, S is top left
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}