using System;
using System.IO;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
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
            // Split the input into a list of strings by a full empty row
            //string[] games = input.Split(new string[] { "\n\n" }, StringSplitOptions.None);

            string inputFile = Path.GetFullPath(@"..\..\..\input.txt");
            string[] input = File.ReadAllLines(inputFile);

            decimal totalCost = 0;

            for (int i = 0; i < input.Length; i += 4)
            {
                // Parse Button A values
                string[] buttonAValues = input[i].Split(new[] { ": X+" }, StringSplitOptions.None);
                decimal buttonAX = decimal.Parse(buttonAValues[1].Split(',')[0]);
                decimal buttonAY = decimal.Parse(buttonAValues[1].Split(',')[1].Split('+')[1]);

                // Parse Button B values
                string[] buttonBValues = input[i + 1].Split(new[] { ": X+" }, StringSplitOptions.None);
                decimal buttonBX = decimal.Parse(buttonBValues[1].Split(',')[0]);
                decimal buttonBY = decimal.Parse(buttonBValues[1].Split(',')[1].Split('+')[1]);

                // Parse Prize values
                string[] prizeValues = input[i + 2].Split(new[] { "=" }, StringSplitOptions.None);
                decimal prizeX = decimal.Parse(prizeValues[1].Substring(0, prizeValues[1].IndexOf(","))); // Take the first number in prizeValues[1] up until the first comma
                decimal prizeY = decimal.Parse(prizeValues[2]);

                // Calculate unit cost for a single game
                decimal bpresses = ((buttonAX * prizeY) - (buttonAY * prizeX)) / ((buttonAX * buttonBY) - (buttonAY * buttonBX));
                decimal apresses = (prizeX - (buttonBX * bpresses)) / buttonAX;
                // I wish I studied more maths. These seemed very complicated at first.. but fuck, this is simple as hell.

                if (apresses % 1 == 0 && bpresses % 1 == 0) totalCost += (apresses * 3) + bpresses;
            }

            Console.WriteLine($"Total cost: {totalCost}");
            Console.WriteLine("Part 1 executed.");
        }
    }

    public class Part2
    {
        public static void Execute()
        {
            string inputFile = Path.GetFullPath(@"..\..\..\input2.txt");
            string[] input = File.ReadAllLines(inputFile);

            decimal totalCost = 0;

            for (int i = 0; i < input.Length; i += 4)
            {
                // Parse Button A values
                string[] buttonAValues = input[i].Split(new[] { ": X+" }, StringSplitOptions.None);
                decimal buttonAX = decimal.Parse(buttonAValues[1].Split(',')[0]);
                decimal buttonAY = decimal.Parse(buttonAValues[1].Split(',')[1].Split('+')[1]);

                // Parse Button B values
                string[] buttonBValues = input[i + 1].Split(new[] { ": X+" }, StringSplitOptions.None);
                decimal buttonBX = decimal.Parse(buttonBValues[1].Split(',')[0]);
                decimal buttonBY = decimal.Parse(buttonBValues[1].Split(',')[1].Split('+')[1]);

                // Parse Prize values, and add the Part 2 increment (10000000000000) to each value
                string[] prizeValues = input[i + 2].Split(new[] { "=" }, StringSplitOptions.None);
                decimal prizeX = 10000000000000 + decimal.Parse(prizeValues[1].Substring(0, prizeValues[1].IndexOf(","))); // Take the first number in prizeValues[1] up until the first comma
                decimal prizeY = 10000000000000 + decimal.Parse(prizeValues[2]);

                // Calculate unit cost for a single game
                decimal bpresses = ((buttonAX * prizeY) - (buttonAY * prizeX)) / ((buttonAX * buttonBY) - (buttonAY * buttonBX));
                decimal apresses = (prizeX - (buttonBX * bpresses)) / buttonAX;
                // I wish I studied more maths. These seemed very complicated at first.. but fuck, this is simple as hell.

                if (apresses % 1 == 0 && bpresses % 1 == 0) totalCost += (apresses * 3) + bpresses;
            }

            Console.WriteLine($"Total cost: {totalCost}");
            Console.WriteLine("Part 2 executed.");
        }
    }
}