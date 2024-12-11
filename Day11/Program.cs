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
            // Split the input into a list of numbers by " "
            List<long> numbers = lines[0].Split(' ').Select(long.Parse).ToList();

            for (int i = 0; i < 25; i++)
            {
                Blink(ref numbers);

                Console.WriteLine($"Blink {i + 1}: Count: {numbers.Count}");
            }

            Console.WriteLine("Part 1 executed.");
        }

        private static void Blink(ref List<long> numbers)
        {
            var newNumbers = new List<long>();
            foreach (var n in numbers)
            {
                if (n == 0)
                {
                    newNumbers.Add(1);
                }
                else if (HasEvenNumberOfDigits(n))
                {
                    var numStr = n.ToString();
                    var middleIndex = numStr.Length / 2;
                    var firstHalf = int.Parse(numStr.Substring(0, middleIndex));
                    var secondHalf = int.Parse(numStr.Substring(middleIndex));

                    newNumbers.Add(firstHalf);
                    newNumbers.Add(secondHalf);
                }
                else
                {
                    newNumbers.Add(n * 2024);
                }
            }
            numbers = newNumbers;
        }

        private static bool HasEvenNumberOfDigits(long number)
        {
            return Math.Abs(number).ToString().Length % 2 == 0;
        }
    }

    public class Part2
    {
        public static void Execute(string[] lines)
        {
            List<long> numbers = lines[0].Split(' ').Select(long.Parse).ToList();
            List<long> result = new List<long>();

            foreach (long number in numbers)
            {
                List<long> blinkedNumbers = BlinkNumber(number, 75);
                result.AddRange(blinkedNumbers);
                Console.WriteLine("Number: " + number + " handled.");
            }

            Console.WriteLine("Result: Count: " + result.Count);
            Console.WriteLine("Part 2 executed.");
        }

        private static List<long> BlinkNumber(long number, int times)
        {
            List<long> numbers = new List<long> { number };

            for (int i = 0; i < times; i++)
            {
                numbers = Blink(numbers);
            }

            return numbers;
        }

        private static List<long> Blink(List<long> numbers)
        {
            List<long> newNumbers = new List<long>();

            foreach (long n in numbers)
            {
                if (n == 0)
                {
                    newNumbers.Add(1);
                }
                else if (HasEvenNumberOfDigits(Math.Abs(n)))
                {
                    string numStr = Math.Abs(n).ToString();
                    int middleIndex = numStr.Length / 2;
                    string firstHalf = numStr.Substring(0, middleIndex);
                    string secondHalf = numStr.Substring(middleIndex);

                    long firstHalfInt = long.Parse(firstHalf);
                    long secondHalfInt = long.Parse(secondHalf);

                    if (n < 0)
                    {
                        firstHalfInt = -firstHalfInt;
                    }

                    newNumbers.Add(firstHalfInt);
                    newNumbers.Add(secondHalfInt);
                }
                else
                {
                    newNumbers.Add(n * 2024);
                }
            }

            return newNumbers;
        }

        private static bool HasEvenNumberOfDigits(long number)
        {
            return Math.Abs(number).ToString().Length % 2 == 0;
        }
    }
}