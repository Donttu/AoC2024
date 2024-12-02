using System;
using System.IO;

namespace DayX
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read input, either "test.txt" or "input.txt"
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "test.txt");
            string lines = File.ReadAllText(filePath);


            Console.WriteLine(lines);

            Console.WriteLine("Choose a part to execute:");
            Console.WriteLine("1. Part 1");
            Console.WriteLine("2. Part 2");

            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    Part1.Execute();
                    break;
                case "2":
                    Part2.Execute();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose a valid part.");
                    break;
            }
        }        
    }

    public class Part1
    {
        public static void Execute()
        {
            Console.WriteLine("Part 1 executed.");
        }
    }

    public class Part2  {
        public static void Execute()
        {
            Console.WriteLine("Part 2 executed.");
        }
    }
}