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

    public class Part1 // 6337921897505
    {
        public static void Execute(string[] asd)
        {
            string inputFile = Path.GetFullPath(@"..\..\..\input.txt");
            string input = File.ReadAllText(inputFile);
            //input = string.Join("", asd);

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

    public class Part2 // 6362722604045
    {
        public static void Execute(string[] lines)
        {
            string inputFile = Path.GetFullPath(@"..\..\..\input.txt");
            string input = File.ReadAllText(inputFile);
            //input = string.Join("", asd);

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
                if (j == 10) { j = 0; }
                int digit = Int32.Parse(numbers[i].ToString());
                for (int k = 0; k < digit; k++)
                {
                    diskLayout.Add(j.ToString());
                }
                i++;
                if (i >= numbers.Count) { break; }
                digit = Int32.Parse(numbers[i].ToString());
                if (j != 9)
                {
                    for (int k = 0; k < digit; k++)
                    {
                        diskLayout.Add('.'.ToString());
                    }
                }
            }

            Console.WriteLine("The disk layout is:");
            Console.WriteLine(string.Join("", diskLayout));

            /*
            TODO: Implement a new type of moving algorithm that moves whole files instead of single blocks.
            A file is basicly all the blocks that have the same value.
            Starting from the file with the highest ID number, we try to see if we can fit that file to a gap of dots to the left of it.
            If there is no gap that is big enough for the single file, we move on to the next file that has an ID that is one lower than the previous one, and leave the original file at its place.
            If we move a file from right to left, the space that the file used to take should be preserved.
            Every file should be tried to moved exatcly once in order of decreasing file ID number.
            */

            int highestFileId = 0;
            foreach (string fileId in diskLayout)
            {
                if (fileId == ".") { continue; }
                if (int.Parse(fileId) > highestFileId)
                {
                    highestFileId = int.Parse(fileId);
                }
            }
            int amountOfFiles = diskLayout.Count(x => x == highestFileId.ToString());

            Console.WriteLine($"Highest File ID: {highestFileId}");
            Console.WriteLine($"Amount of Files with Highest ID: {amountOfFiles}");
            
            Console.WriteLine("The disk layout after moving file blocks is:");
            Console.WriteLine(string.Join("", diskLayout));

            Console.WriteLine(diskLayout.Count);

            long checksum = 0;
            /*
            for (int i = 0; i < diskLayout.Count; i++)
            {
                if (char.IsDigit(diskLayout[i][0]))
                {
                    checksum += i * (diskLayout[i][0] - '0');
                }
            }
            */

            Console.WriteLine("The checksum is: " + checksum);
            Console.WriteLine("Part 2 executed.");
        }
    }
}