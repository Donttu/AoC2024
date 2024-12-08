using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Day07
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
            double sum = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(':');
                if (parts.Length != 2) continue;

                double target = double.Parse(parts[0]);
                double[] numbers = parts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => double.Parse(s)).ToArray();
                if (CanCreateNumber(target, numbers))
                {
                    sum += target;
                }

            }

            Console.WriteLine(sum);
            Console.WriteLine("Part 1 executed.");
        }

        public static bool CanCreateNumber(double target, double[] numbers)
        {
            return CanCreateNumberRecursive(target, numbers, 0, 0);
        }

        public static bool CanCreateNumberRecursive(double target, double[] numbers, int index, double current)
        {
            if (index == numbers.Length)
            {
                return current == target;
            }

            return CanCreateNumberRecursive(target, numbers, index + 1, current + numbers[index]) ||
                   CanCreateNumberRecursive(target, numbers, index + 1, current * numbers[index]);
        }
    }

    public class Part2
    {
        public static void Execute(string[] lines)
        {
            double sum = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(':');
                if (parts.Length != 2) continue;

                double target = double.Parse(parts[0]);
                double[] numbers = parts[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => double.Parse(s)).ToArray();
                if (CanCreateNumber(target, numbers))
                {
                    sum += target;
                }

            }
            Console.WriteLine(sum);
            Console.WriteLine("Part 2 executed.");
        }

        public static bool CanCreateNumber(double target, double[] numbers)
        {
            return CanCreateNumberRecursive(target, numbers, 0, 0);
        }


        public static bool CanCreateNumberRecursive(double target, double[] numbers, int index, double current)
        {
            if (index == numbers.Length)
            {
                return current == target;
            }

            return CanCreateNumberRecursive(target, numbers, index + 1, current + numbers[index]) ||
                   CanCreateNumberRecursive(target, numbers, index + 1, current * numbers[index]) ||
                   CanCreateNumberRecursive(target, numbers, index + 1, current * Math.Pow(10, numbers[index].ToString().Length) + numbers[index]);
        }
    }
}