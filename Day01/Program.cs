using System;
using System.IO;

namespace Day1
{
    class Program
    {
        /// <summary>
        /// Main entry point. Reads input from file, and based on user choice, executes
        /// either Part1 or Part2.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
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

            Console.WriteLine(lines.Length);
            
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
        /// Reads the input content, splits it into lines, and then for each line, it
        /// parses the two numbers, calculates the difference, and sums this difference
        /// with the previous ones. Finally, it prints the sum of differences.
        /// </summary>
        /// <param name="content">The content of the input file.</param>
        public static void Execute(string[] lines)
        {
            int[][] pairs = GetPairedNumbers(lines);

            int sumOfDifferences = 0;
            foreach (int[] pair in pairs)
            {
                int difference = Math.Max(pair[0], pair[1]) - Math.Min(pair[0], pair[1]);
                sumOfDifferences += difference;
                //Console.WriteLine($"Difference: {difference}");
            }

            Console.WriteLine($"Sum of differences: {sumOfDifferences}");

            
            Console.WriteLine("Part 1 executed.");
        }

        /// <summary>
        /// Given an array of strings, each containing two numbers separated by a space,
        /// returns an array of int arrays, each containing two numbers.
        /// The numbers in the input array are sorted in ascending order within their respective columns.
        /// </summary>
        /// <param name="lines">Array of strings, each containing two numbers separated by a space.</param>
        /// <returns>An array of int arrays, each containing two numbers.</returns>
        private static int[][] GetPairedNumbers(string[] lines)
        {
            int[] leftColumn = new int[lines.Length];
            int[] rightColumn = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] numbers = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                leftColumn[i] = int.Parse(numbers[0]);
                rightColumn[i] = int.Parse(numbers[1]);
            }

            Array.Sort(leftColumn);
            Array.Sort(rightColumn);

            int[][] pairs = new int[leftColumn.Length][];
            for (int i = 0; i < leftColumn.Length; i++)
            {
                pairs[i] = new int[] { leftColumn[i], rightColumn[i] };
            }

            return pairs;
        }
    }

    public class Part2  {
        /// <summary>
        /// Executes part 2 of the day's puzzle.
        /// Splits the input content into lines, extracts the left and right columns of numbers,
        /// calculates the score based on the numbers in both columns, and prints the score.
        /// </summary>
        /// <param name="content">The content of the input file.</param>
        public static void Execute(string[] lines)
        {
            int[] leftColumn = GetLeftColumn(lines);
            int[] rightColumn = GetRightColumn(lines);

            int score = CalculateScore(leftColumn, rightColumn);
            Console.WriteLine($"Score: {score}");
            
            Console.WriteLine("Part 2 executed.");
        }

        /// <summary>
        /// Extracts the left column of numbers from the input lines.
        /// Splits each line into two numbers, and then extracts the first number
        /// into a new array.
        /// </summary>
        /// <param name="lines">An array of strings, each containing two numbers
        /// separated by a space.</param>
        /// <returns>An array of integers, where each element is the left number
        /// from the corresponding line in <paramref name="lines"/>.</returns>
        private static int[] GetLeftColumn(string[] lines)
        {
            int[] leftColumn = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] numbers = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                leftColumn[i] = int.Parse(numbers[0]);
            }
            return leftColumn;
        }

        /// <summary>
        /// Extracts the right column of numbers from the input lines.
        /// Splits each line into two numbers, and then extracts the second number
        /// into a new array.
        /// </summary>
        /// <param name="lines">An array of strings, each containing two numbers
        /// separated by a space.</param>
        /// <returns>An array of integers, where each element is the right number
        /// from the corresponding line in <paramref name="lines"/>.</returns>
        private static int[] GetRightColumn(string[] lines)
        {
            int[] rightColumn = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] numbers = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                rightColumn[i] = int.Parse(numbers[1]);
            }
            return rightColumn;
        }

        /// <summary>
        /// Calculates a score based on the two columns of numbers.
        /// For each number in the left column, it counts the number of occurrences
        /// of that number in the right column, and adds the product of the number
        /// and the count to the score.
        /// </summary>
        /// <param name="leftColumn">The array of numbers in the left column.</param>
        /// <param name="rightColumn">The array of numbers in the right column.</param>
        /// <returns>The calculated score.</returns>
        private static int CalculateScore(int[] leftColumn, int[] rightColumn)
        {
            int score = 0;
            foreach (int number in leftColumn)
            {
                int count = rightColumn.Count(x => x == number);
                score += number * count;
            }
            return score;
        }
    }
}