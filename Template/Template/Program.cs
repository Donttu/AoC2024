using System;
using System.IO;

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
            Console.WriteLine("Part 1 executed.");
        }
    }

    public class Part2  {
        public static void Execute(string[] lines)
        {
            Console.WriteLine("Part 2 executed.");
        }
    }
}