using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Day03
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
        /// Reads the input, extracts all multiplication instructions, and sums their products.
        /// Prints the total sum.
        /// </summary>
        /// <param name="lines">Array of strings, each containing a line of input.</param>
        public static void Execute(string[] lines)
        {
            int totalSum = 0;
            string input = string.Join("", lines);

            var matches = Regex.Matches(input, "mul\\(([0-9]+),([0-9]+)\\)"); // Extract all multiplication instructions
            foreach (Match match in matches)
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                totalSum += x * y;
            }

            Console.WriteLine($"Total Sum: {totalSum}");
            Console.WriteLine("Part 1 executed.");
        }
    }

    public class Part2
    {
        /// <summary>
        /// Executes part 2 of the day's puzzle.
        /// Reads the input, removes all instructions between "don't()" and "do()", and sums the products of all "mul(x, y)" instructions.
        /// Prints the total sum.
        /// </summary>
        /// <param name="lines">Array of strings, each containing a line of input.</param>
        public static void Execute(string[] lines)
        {
            int totalSum = 0;
            string input = string.Join("", lines);

            string cleanedInput = Regex.Replace(input, @"don't\(\).*?do\(\)", ""); // Remove instructions between "don't()" and "do()"

            var matches = Regex.Matches(cleanedInput, "mul\\(([0-9]+),([0-9]+)\\)"); // Extract all multiplication instructions
            foreach (Match match in matches)
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                totalSum += x * y;
            }

            Console.WriteLine($"Total Sum: {totalSum}");
            Console.WriteLine("Part 2 executed.");
        }
    }
}