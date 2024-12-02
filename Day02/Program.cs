using System;
using System.IO;

namespace Day2
{
    class Program
    {
        /// <summary>
        /// Main entry point for the program. Prompts the user to choose a part to execute
        /// and reads input data from the console.
        /// Depending on the user's choice, executes either Part1 or Part2 logic, passing
        /// the input data as an array of strings, where each string represents a line of input.
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
        /// For each line in the input, it checks if the line is a safe report.
        /// A report is safe if the numbers in the report are either always increasing
        /// or always decreasing by at least 1 and at most 3.
        /// The number of safe reports is then printed.
        /// </summary>
        /// <param name="lines">An array of strings, each containing a report.</param>
        public static void Execute(string[] lines)
        {
            int safeCount = 0;
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim(); // Remove newline character
                
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    int[] numbers = line.Split(' ').Select(int.Parse).ToArray(); // Split line into numbers

                    bool increasing = true;
                    bool decreasing = true;

                    for (int i = 1; i < numbers.Length; i++) // Check if the report is safe
                    {
                        int difference = Math.Abs(numbers[i] - numbers[i - 1]); // Calculate the difference between two consecutive numbers
                        if (difference > 3 || difference < 1) // Check if the difference is greater than 3 or less than 1
                        {
                            increasing = decreasing = false; // If the difference is greater than 3 or less than 1, the report is not safe
                            break;
                        }
                        else if (numbers[i] > numbers[i - 1]) // Check if the current number is greater than the previous number
                        {
                            decreasing = false; // If the current number is greater than the previous number, the report is not decreasing-safe
                        }
                        else if (numbers[i] < numbers[i - 1]) // Check if the current number is less than the previous number
                        {
                            increasing = false; // If the current number is less than the previous number, the report is not increasing-safe
                        }
                    }
                
                    if (increasing || decreasing)
                    {
                        safeCount++;
                    }
                }
            }
            Console.WriteLine($"Number of safe reports: {safeCount}");
            Console.WriteLine("Part 1 executed.");
        }
    }

    public class Part2  {
        /// <summary>
        /// Executes part 2 of the day's puzzle.
        /// For each line in the input, it checks if the line is a safe report.
        /// A report is safe if removing any one number in the report makes the
        /// resulting report satisfy the rules of part 1.
        /// The number of safe reports is then printed.
        /// </summary>
        /// <param name="lines">An array of strings, each containing a report.</param>
        public static void Execute(string[] lines)
        {
            int safeCount = 0;
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim(); // Remove newline character
                
                if (!string.IsNullOrEmpty(trimmedLine))
                {
                    int[] numbers = trimmedLine.Split(' ').Select(int.Parse).ToArray(); // Split line into numbers
                   
                    bool isSafe = false;

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        int[] newNumbers = new int[numbers.Length - 1]; // Create new array with one less element
                        Array.Copy(numbers, 0, newNumbers, 0, i); // Copy elements before i
                        Array.Copy(numbers, i + 1, newNumbers, i, numbers.Length - i - 1); // Copy elements after i

                        bool increasing = true;
                        bool decreasing = true;

                        for (int j = 1; j < newNumbers.Length; j++) // Check if the resulting report is safe
                        {
                            int difference = Math.Abs(newNumbers[j] - newNumbers[j - 1]); // Calculate the difference between two consecutive numbers
                            if (difference > 3 || difference < 1) // Check if the difference is greater than 3 or less than 1
                            {
                                increasing = decreasing = false; // If the difference is greater than 3 or less than 1, the resulting report is not safe
                                break;
                            }
                            else if (newNumbers[j] > newNumbers[j - 1]) // Check if the current number is greater than the previous number
                            {
                                decreasing = false; // If the current number is greater than the previous number, the resulting report is not decreasing-safe
                            }
                            else if (newNumbers[j] < newNumbers[j - 1]) // Check if the current number is less than the previous number
                            {
                                increasing = false; // If the current number is less than the previous number, the resulting report is not increasing-safe
                            }
                        }

                        if (increasing || decreasing)
                        {
                            isSafe = true;
                            break;
                        }
                    }

                    if (isSafe)
                    {
                        safeCount++;
                    }

                }
            }      
            Console.WriteLine($"Number of safe reports: {safeCount}");
            Console.WriteLine("Part 2 executed.");
        }
    }
}