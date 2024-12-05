using System;
using System.IO;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            Parts.Execute();
        }
    }

    public class Parts
    {
        /// <summary>
        /// Executes part 1 and part 2 of the day's puzzle.
        /// For part 1, reads the input, parses it into a list of rules and a list of data.
        /// Checks if the data lines match all the rules. 
        /// If a data line matches all the rules, it adds the middle value of the data line into a list.
        /// Finally, prints the sum of all middle values.
        /// For part 2, does the same as part 1 but sorts the data line according to the rules if it
        /// does not match the rules initially. Then, prints the sum of all middle values.
        /// </summary>
        public static void Execute()
        {
            string inputFile = Path.GetFullPath(@"..\..\..\input.txt");
            string[] lines = File.ReadAllLines(inputFile);

            string[] rules = lines.TakeWhile(x => x != "").ToArray();
            string[] data = lines.Skip(rules.Length + 1).ToArray();

            Console.WriteLine($"Rules length: {rules.Length}"); // Debug
            Console.WriteLine($"Data length: {data.Length}"); // Debug

            var rulesList = new List<string[]>(); // List of rules
            foreach (var rule in rules) // Parse rules into list of rules
            {
                var parts = rule.Split('|');
                rulesList.Add(new string[] { parts[0], parts[1] });
            }

            var middleValues = new List<int>(); // Empty list for middle values
            var correctedMiddleValues = new List<int>(); // Empty list for corrected middle values

            foreach (var line in data) // Loop through data
            {
                var numbers = line.Split(',').Select(int.Parse).ToArray(); // Parse data line into array of numbers
                bool isValid = true; 
                foreach (var rule in rulesList) // Loop through rules
                {
                    int x = int.Parse(rule[0]); 
                    int y = int.Parse(rule[1]);
                    if (Array.IndexOf(numbers, x) != -1 && Array.IndexOf(numbers, y) != -1 && Array.IndexOf(numbers, x) > Array.IndexOf(numbers, y)) // Check if data does not match the rule
                    {
                        isValid = false; // If data does not match the rule, set isValid to false
                        break;
                    }
                }
                if (isValid) // If data matches the rule
                {
                    middleValues.Add(numbers[numbers.Length / 2]); // Add middle value to list
                }
                else // If data does not match the rule -> isValid = false
                {
                    var correctOrder = (int[])numbers.Clone(); // Create copy of numbers
                    bool swapped;
                    do // Sort numbers
                    {
                        swapped = false;
                        foreach (var rule in rulesList) // Loop through rules
                        {
                            int indexX = Array.IndexOf(correctOrder, int.Parse(rule[0])); // Get index of x
                            int indexY = Array.IndexOf(correctOrder, int.Parse(rule[1])); // Get index of y
                            if (indexX != -1 && indexY != -1 && indexX > indexY) // Line does not match the rule
                            {
                                int temp = correctOrder[indexX]; // Swap x and y
                                correctOrder[indexX] = correctOrder[indexY]; 
                                correctOrder[indexY] = temp;
                                swapped = true;
                            }
                        }
                    } while (swapped); // Repeat until no more swaps
                    correctedMiddleValues.Add(correctOrder[correctOrder.Length / 2]);
                }
            }

            int sum = middleValues.Sum();
            Console.WriteLine(sum);
            Console.WriteLine("Part 1 executed.");

            int correctedSum = correctedMiddleValues.Sum();
            Console.WriteLine(correctedSum);
            Console.WriteLine("Part 2 executed.");
        }
    }
}