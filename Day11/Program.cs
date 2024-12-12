using System;
using System.IO;
using System.Numerics;

namespace Day11
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
            var input = lines[0].Trim().Split(' ').Select(long.Parse).ToList();
            var part2 = Part02(input);
            Console.WriteLine($"Part 2: {part2}");
            Console.WriteLine("Part 2 executed.");
        }

        private static long Part02(IReadOnlyList<long> input) => Blink(input, 75);

        private static long Blink(IReadOnlyList<long> input, int times)
        {
            // Initialize an empty dictionary to store stones and their counts
            var stonesDict = new Dictionary<long, long>();

            // Add the initial stones to the dictionary
            foreach (var b in input)
            {
                Add(b, 1);
            }

            // Loop through the number of times (blinks) specified
            for (var i = 0; i < times; i++)
            {
                // Iterate through a copy of the dictionary
                foreach (var (key, count) in stonesDict.ToList())
                {
                    // Remove the current stone key and its count
                    Remove(key, count);

                    // If the key is 0, add a stone with key 1
                    if (key == 0)
                    {
                        Add(1, count);
                    }
                    else
                    {
                        // Convert the key to a string to check its length
                        var digits = key.ToString();

                        // If the number of digits is odd, multiply the key by 2024 and add it
                        if (digits.Length % 2 == 1)
                        {
                            Add(key * 2024, count);
                        }
                        // If the number of digits is even, split the digits in half and parse each half as a new key   
                        else
                        {
                            Add(long.Parse(digits.Substring(0, digits.Length / 2)), count);
                            Add(long.Parse(digits.Substring(digits.Length / 2)), count);
                        }
                    }
                }
            }

            // Return the sum of all values in the dictionary
            return stonesDict.Values.Sum();

            /// <summary>
            /// Adds a key-value pair to the dictionary, or updates the value if the key already exists.
            /// </summary>
            /// <param name="key">The key to add or update.</param>
            /// <param name="value">The value to add or update.</param>
            void Add(long key, long value)
            {
                stonesDict.TryAdd(key, 0);
                stonesDict[key] += value;
            }

            /// <summary>
            /// Removes a key-value pair from the dictionary if the value is 0.
            /// </summary>
            /// <param name="key">The key to remove.</param>
            /// <param name="value">The value to remove.</param>
            void Remove(long key, long value) => stonesDict[key] -= value;
        }
    }
}