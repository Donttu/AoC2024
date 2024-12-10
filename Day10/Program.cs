using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DayX
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
        public static void Execute(string[] lines)
        {
            // Turn the lines into char matrix
            char[,] matrix = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                }
            }

            // Find all zeroes in the matrix: They are the starting points.
            List<(int, int)> zeroes = new List<(int, int)>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == '0')
                    {
                        zeroes.Add((i, j));
                    }
                }
            }

            int score = 0;
            foreach (var (x, y) in zeroes)
            {
                score += CalculateScore(matrix, x, y);
                Console.WriteLine($"Score after ({x}, {y}): {score}");
            }

            // print score
            Console.WriteLine($"Score: {score}");

            Console.WriteLine("Part 1 executed.");
        }

        private static int CalculateScore(char[,] matrix, int startX, int startY)
        {
            int score = 0;
            bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            ExplorePaths(matrix, -1, -1, -1, startX, startY, ref score, visited);
            return score;
        }

        private static void ExplorePaths(char[,] matrix, int previousX, int previousY, int previousHeight, int currentX, int currentY, ref int score, bool[,] visited)
        {
            // If-statement to check whether current X and Y are a valid position in the matrix
            if (currentX < 0 || currentX >= matrix.GetLength(0) || currentY < 0 || currentY >= matrix.GetLength(1))
            {
                return;
            }

            // If-statement to check that if previousX, previousY and previousHeigth are all -1, we should skip to the else part.
            if (previousX == -1 && previousY == -1 && previousHeight == -1)
            {
                // This is the first call to the function, so we should be able to skip to the else part.
                goto ElsePart;
            }

            // If-statement to check that the currentHeight is larger than previousHeight by one
            if (matrix[currentX, currentY] - '0' != previousHeight + 1)
            {
                return;
            }

            if (visited[currentX, currentY]) // If the position has already been visited, return
            {
                return;
            }

            visited[currentX, currentY] = true;

            if (matrix[currentX, currentY] - '0' == 9 && previousHeight == 8) // If the current position is a 9 and the previous height is 8, increase the score
            {
                score++;
            }

        ElsePart:
            ExplorePaths(matrix, currentX, currentY, matrix[currentX, currentY] - '0', currentX - 1, currentY, ref score, visited); // left
            ExplorePaths(matrix, currentX, currentY, matrix[currentX, currentY] - '0', currentX + 1, currentY, ref score, visited); // right
            ExplorePaths(matrix, currentX, currentY, matrix[currentX, currentY] - '0', currentX, currentY - 1, ref score, visited); // up
            ExplorePaths(matrix, currentX, currentY, matrix[currentX, currentY] - '0', currentX, currentY + 1, ref score, visited); // down
        }
    }


    public class Part2
    {

        public static void Execute(string[] lines)
        {
            // Turn the lines into char matrix
            char[,] matrix = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                }
            }

            // Find all zeroes in the matrix: They are the starting points.
            List<(int, int)> zeroes = new List<(int, int)>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == '0')
                    {
                        zeroes.Add((i, j));
                    }
                }
            }

            int score = 0;
            foreach (var (x, y) in zeroes)
            {
                score += CalculateScore(matrix, x, y);
                Console.WriteLine($"Score after ({x}, {y}): {score}");
            }

            // print score
            Console.WriteLine($"Score: {score}");

            Console.WriteLine("Part 2 executed.");
        }

        private static int CalculateScore(char[,] matrix, int startX, int startY)
        {
            int score = 0;
            ExplorePaths(matrix, -1, -1, -1, startX, startY, ref score);
            return score;
        }

        private static void ExplorePaths(char[,] matrix, int previousX, int previousY, int previousHeight, int currentX, int currentY, ref int score)
        {
            // TODO: If-statement to check whether current X and Y are a valid position in the matrix
            if (currentX < 0 || currentX >= matrix.GetLength(0) || currentY < 0 || currentY >= matrix.GetLength(1))
            {
                return;
            }

            // TODO: If-statement to check that if previousX, previousY and previousHeigth are all -1, we should skip to the else part.
            if (previousX == -1 && previousY == -1 && previousHeight == -1)
            {
                // This is the first call to the function, so we should be able to skip to the else part.
                goto ElsePart;
            }

            //TODO: If-statement to check that the currentHeight is larger than previousHeight by one
            if (matrix[currentX, currentY] - '0' != previousHeight + 1)
            {
                return;
            }

            if (matrix[currentX, currentY] - '0' == 9 && previousHeight == 8)
            {
                score++;
            }

        ElsePart:
            ExplorePaths(matrix, currentX, currentY, matrix[currentX, currentY] - '0', currentX - 1, currentY, ref score); // left
            ExplorePaths(matrix, currentX, currentY, matrix[currentX, currentY] - '0', currentX + 1, currentY, ref score); // right
            ExplorePaths(matrix, currentX, currentY, matrix[currentX, currentY] - '0', currentX, currentY - 1, ref score); // up
            ExplorePaths(matrix, currentX, currentY, matrix[currentX, currentY] - '0', currentX, currentY + 1, ref score); // down
        }
    }
}