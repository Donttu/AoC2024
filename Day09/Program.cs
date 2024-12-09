using System;
using System.Globalization;
using System.IO;
using System.Net.Http.Headers;
using System.Numerics;

namespace Day09
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
        /// <summary>
        /// Executes part 1 of the day's puzzle.
        /// Reads the input line, converts it into a list of numbers.
        /// Creates a disk layout by repeating the numbers.
        /// Moves the file blocks to the left
        /// Calculates the checksum by summing the index of each number times the number.
        /// Prints the checksum.
        /// </summary>
        public static void Execute()
        {
            string inputFile = Path.GetFullPath(@"..\..\..\input.txt");
            string input = File.ReadAllText(inputFile);

            List<int> numbers = new List<int>();
            foreach (char x in input)
            {
                numbers.Add(int.Parse(x.ToString()));
            }

            Console.WriteLine("The numbers are:");
            foreach (int number in numbers)
            {
                Console.Write(number);
            }
            Console.WriteLine();

            List<string> diskLayout = new List<string>();

            for (int i = 0, j = 0; i < numbers.Count; i++, j++)
            {
                //if (j == 10) { j = 0; }
                int digit = Int32.Parse(numbers[i].ToString());
                for (int k = 0; k < digit; k++)
                {
                    diskLayout.Add(j.ToString());
                }
                i++;
                if (i >= numbers.Count) { break; }
                digit = Int32.Parse(numbers[i].ToString());
                if (j != -1)
                {
                    for (int k = 0; k < digit; k++)
                    {
                        diskLayout.Add('.'.ToString());
                    }
                }
            }

            Console.WriteLine("The disk layout is:");
            Console.WriteLine(string.Join("", diskLayout));

            while (true)
            {
                int lastNonDotIndex = -1;
                for (int i = diskLayout.Count - 1; i >= 0; i--)
                {
                    if (diskLayout[i] != '.'.ToString())
                    {
                        lastNonDotIndex = i;
                        break;
                    }
                }

                int firstDotIndex = -1;
                for (int i = 0; i < diskLayout.Count; i++)
                {
                    if (diskLayout[i] == '.'.ToString())
                    {
                        firstDotIndex = i;
                        break;
                    }
                }

                if (lastNonDotIndex < firstDotIndex)
                {
                    break;
                }

                string temp = diskLayout[lastNonDotIndex];
                diskLayout[lastNonDotIndex] = diskLayout[firstDotIndex];
                diskLayout[firstDotIndex] = temp;
            }

            Console.WriteLine("The disk layout after moving file blocks is:");
            Console.WriteLine(string.Join("", diskLayout));

            long checksum = 0;
            for (int i = 0; i < diskLayout.Count; i++)
            {
                if (diskLayout[i] == '.'.ToString()) { break; }
                checksum += i * long.Parse(diskLayout[i]);
            }

            Console.WriteLine("The checksum is: " + checksum);
            Console.WriteLine("Part 1 executed.");
        }
    }

    public class Part2
    {
        /// <summary>
        /// Executes part 2 of the day's puzzle.
        /// Reads the input file and parses it into a list of numbers.
        /// Creates a disk layout by repeating numbers and adding gaps.
        /// Shifts file blocks to fill appropriate gaps based on their field IDs.
        /// Calculates the checksum by summing the index of each number multiplied by the number itself.
        /// Prints the checksum.
        /// </summary>
        public static void Execute()
        {
            string inputFile = Path.GetFullPath(@"..\..\..\input.txt");
            string input = File.ReadAllText(inputFile);

            List<int> numbers = new List<int>();
            foreach (char x in input)
            {
                numbers.Add(int.Parse(x.ToString()));
            }

            Console.WriteLine("The numbers are:");
            foreach (int number in numbers)
            {
                Console.Write(number);
            }
            Console.WriteLine();

            List<string> diskLayout = new List<string>();

            for (int i = 0, j = 0; i < numbers.Count; i++, j++)
            {
                int digit = Int32.Parse(numbers[i].ToString());
                for (int k = 0; k < digit; k++)
                {
                    diskLayout.Add(j.ToString());
                }
                i++;
                if (i >= numbers.Count) { break; }
                digit = Int32.Parse(numbers[i].ToString());
                for (int k = 0; k < digit; k++)
                {
                    diskLayout.Add('.'.ToString());
                }
            }

            int highestFieldId = 0;
            foreach (string fileId in diskLayout)
            {
                if (fileId == ".")
                {
                    continue;
                }
                if (int.Parse(fileId) > highestFieldId)
                {
                    highestFieldId = int.Parse(fileId);
                }
            }

            while (highestFieldId >= 0)
            {
                int highFileId = highestFieldId;
                int amountOfFiles = diskLayout.Count(x => x == highFileId.ToString());
                int requiredGapSize = amountOfFiles;
                int highFileIdIndex = -1;

                for (int i = 0; i < diskLayout.Count; i++)
                {
                    if (diskLayout[i] == highFileId.ToString())
                    {
                        highFileIdIndex = i;
                        break;
                    }
                }

                int gapStartIndex = -1;
                int actualGapSize = 0;
                for (int i = 0; i < highFileIdIndex; i++)
                {
                    if (diskLayout[i] == ".")
                    {
                        actualGapSize++;
                        if (actualGapSize == requiredGapSize)
                        {
                            gapStartIndex = i - requiredGapSize + 1;
                            break;
                        }
                    }
                    else if (diskLayout[i] != ".")
                    {
                        actualGapSize = 0;
                    }
                }

                if (gapStartIndex != -1 && highFileIdIndex != -1)
                {
                    for (int i = 0; i < amountOfFiles; i++)
                    {
                        string file = diskLayout[highFileIdIndex + i];
                        string dot = diskLayout[gapStartIndex + i];
                        diskLayout[highFileIdIndex + i] = dot;
                        diskLayout[gapStartIndex + i] = file;
                    }
                }

                highestFieldId--;
            }

            long checksum = 0;

            for (int i = 0; i < diskLayout.Count; i++)
            {
                if (char.IsDigit(diskLayout[i][0]))
                {
                    checksum += i * Int32.Parse(diskLayout[i]);
                }
            }

            Console.WriteLine("The checksum is: " + checksum);
            Console.WriteLine("Part 2 executed.");
        }
    }
}