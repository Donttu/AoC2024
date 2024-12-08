using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Day06
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
            // Create and "fill" matrix
            char[,] matrix = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                }
            }

            // Find the position of the guard
            char target = '^';
            int guardX = 0, guardY = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == target)
                    {
                        guardX = j;
                        guardY = i;
                    }
                }
            }

            int direction = 0; // 0: up, 1: right, 2: down, 3: left
            HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>(); // Keep track of visited positions

            // Simulate the guard's movement
            while (true)
            {
                visitedPositions.Add((guardX, guardY));
                
                int newX = guardX, newY = guardY; // Next position
                switch (direction)
                {
                    case 0: newY--; break; // up
                    case 1: newX++; break; // right
                    case 2: newY++; break; // down
                    case 3: newX--; break; // left
                }

                if (newX < 0 || newX >= matrix.GetLength(1) || newY < 0 || newY >= matrix.GetLength(0)) // If out of bounds
                {
                    break; // If out of bounds, stop moving
                }
                else if (matrix[newY, newX] == '#') // If there's something in front
                {
                    // If there's something in front, turn right
                    direction = (direction + 1) % 4;
                }
                else 
                {
                    // Otherwise, move forward
                    guardX = newX;
                    guardY = newY;
                }
            }

            Console.WriteLine($"The guard will visit {visitedPositions.Count} distinct positions.");
        }
    }


}

public class Part2
{
    public static void Execute(string[] lines)
    {
        // Create and "fill" matrix
        char[,] matrix = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                matrix[i, j] = lines[i][j];
            }
        }

        // Find the position of the guard
        char target = '^';
        int startX = 0, startY = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == target)
                {
                    startX = j;
                    startY = i;
                }
            }
        }

        int count = 0; // Count the number of distinct positions for an extra '#'

        // Iterate over all the possible positions for an extra '#'
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                // Add an extra '#' to the matrix
                if (!(matrix[i, j] == '^') && !(matrix[i, j] == '#'))    {
                    matrix[i, j] = '#';
                }              

                // Simulate the guard's movement
                int guardX = startX, guardY = startY; // Set the initial position of the guard, saving the original coordinates
                int direction = 0; // 0: up, 1: right, 2: down, 3: left
                HashSet<(int, int)> visitedPositions = new HashSet<(int, int)>(); // Keep track of visited positions
                int steps = 0; // Count the number of steps

                // Simulate the guard's movement
                while (true)
                {
                    visitedPositions.Add((guardX, guardY));
                    
                    int newX = guardX, newY = guardY; // Next position
                    switch (direction)
                    {
                        case 0: newY--; break; // up
                        case 1: newX++; break; // right
                        case 2: newY++; break; // down
                        case 3: newX--; break; // left
                    }

                    if (newX < 0 || newX >= matrix.GetLength(1) || newY < 0 || newY >= matrix.GetLength(0)) // If out of bounds
                    {
                        break; // If out of bounds, stop moving
                    }
                    else if (matrix[newY, newX] == '#') // If there's '#' in front
                    {
                        // If there's something in front, turn right
                        direction = (direction + 1) % 4;
                    }
                    else
                    {
                        // Otherwise, move forward
                        guardX = newX;
                        guardY = newY;
                        steps++;
                    }                    

                    if (steps > matrix.GetLength(0) * matrix.GetLength(1)) // If the guard has moved more than the size of the matrix
                    {
                        count++; // This extra '#' will create an infinite loop, so increase count by one.
                        break; // Break the loop
                    }
                }
                // Remove the extra '#' from the matrix
                matrix[i, j] = lines[i][j];
            }
        }
        Console.WriteLine($"There are {count} positions where adding a '#' would get the guard stuck in a loop.");
        Console.WriteLine("Part 2 executed.");
    }
}