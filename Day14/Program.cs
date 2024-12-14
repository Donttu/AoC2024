using System;
using System.IO;

namespace Day14
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
            // Create robots
            List<Robot> robots = new List<Robot>();
            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, @"p=([0-9\-]+),([0-9\-]+) v=([0-9\-]+),([0-9\-]+)");
                if (match.Success)
                {
                    int px = int.Parse(match.Groups[1].Value);
                    int py = int.Parse(match.Groups[2].Value);
                    int vx = int.Parse(match.Groups[3].Value);
                    int vy = int.Parse(match.Groups[4].Value);
                    Robot robot = new Robot((px, py), (vx, vy));
                    robots.Add(robot);
                }
            }

            // create a [height, width] char[,] matrix filled with '.'
            char[,] matrix = new char[103, 101];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = '.';
                }
            }

            int seconds = 100;
            for (int sec = 1; sec <= seconds; sec++)
            {
                foreach (var robot in robots)
                {
                    // Update robot position based on velocity
                    robot.Position = (
                        (robot.Position.x + robot.Velocity.x + matrix.GetLength(1)) % matrix.GetLength(1),
                        (robot.Position.y + robot.Velocity.y + matrix.GetLength(0)) % matrix.GetLength(0)
                    );
                }
            }

            // Place robots on the matrix
            foreach (var robot in robots)
            {
                if (matrix[robot.Position.y, robot.Position.x] == '.')
                {
                    matrix[robot.Position.y, robot.Position.x] = '1';
                }
                else
                {
                    matrix[robot.Position.y, robot.Position.x]++;
                }
            }

            // Calculate the amount of robots in each quadrant of the matrix
            int robotsTopLeft = 0;
            int robotsTopRight = 0;
            int robotsBottomLeft = 0;
            int robotsBottomRight = 0;  
                      
            foreach (var robot in robots)
            {
                if (robot.Position.x < matrix.GetLength(1) / 2 && robot.Position.y < matrix.GetLength(0) / 2)
                {
                    robotsTopLeft++;
                }
                else if (robot.Position.x > matrix.GetLength(1) / 2 && robot.Position.y < matrix.GetLength(0) / 2)
                {
                    robotsTopRight++;
                }
                else if (robot.Position.x < matrix.GetLength(1) / 2 && robot.Position.y > matrix.GetLength(0) / 2)
                {
                    robotsBottomLeft++;
                }
                else if (robot.Position.x > matrix.GetLength(1) / 2 && robot.Position.y > matrix.GetLength(0) / 2)
                {
                    robotsBottomRight++;
                }
            }
            int total = robotsTopLeft * robotsTopRight * robotsBottomLeft * robotsBottomRight;

            Console.WriteLine($"Safety Factor: {total}");
            Console.WriteLine("Part 1 executed.");
        }

        public static void PrintMatrix(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == '.')
                    {
                        Console.Write("\x1B[32m" + matrix[i, j] + "\x1B[0m"); // green color for dots
                    }
                    else
                    {
                        Console.Write("\x1B[31m" + matrix[i, j] + "\x1B[0m"); // red color for other characters
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public class Robot
    {
        public (int x, int y) Position { get; set; }
        public (int x, int y) Velocity { get; set; }

        public Robot((int x, int y) position, (int x, int y) velocity)
        {
            Position = position;
            Velocity = velocity;
        }
    }

    public class Part2
    {
        public static void Execute(string[] lines)
        {
            List<Robot> robots = new List<Robot>();
            foreach (var line in lines)
            {
                var match = System.Text.RegularExpressions.Regex.Match(line, @"p=([0-9\-]+),([0-9\-]+) v=([0-9\-]+),([0-9\-]+)");
                if (match.Success)
                {
                    int px = int.Parse(match.Groups[1].Value);
                    int py = int.Parse(match.Groups[2].Value);
                    int vx = int.Parse(match.Groups[3].Value);
                    int vy = int.Parse(match.Groups[4].Value);
                    Robot robot = new Robot((px, py), (vx, vy));
                    robots.Add(robot);
                }
            }

            // create a [height, width] char[,] matrix filled with '.'
            char[,] matrix = new char[103, 101];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = '.';
                }
            }

            int seconds = 1;

            while (true)
            {
                foreach (var robot in robots)
                {
                    // Update robot position based on velocity
                    robot.Position = (
                        (robot.Position.x + robot.Velocity.x + matrix.GetLength(1)) % matrix.GetLength(1),
                        (robot.Position.y + robot.Velocity.y + matrix.GetLength(0)) % matrix.GetLength(0)
                    );
                }

                // Clear matrix
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = '.';
                    }
                }

                // Place robots on the matrix
                foreach (var robot in robots)
                {
                    if (matrix[robot.Position.y, robot.Position.x] == '.')
                    {
                        matrix[robot.Position.y, robot.Position.x] = '1';
                    }
                    else
                    {
                        matrix[robot.Position.y, robot.Position.x]++;
                    }
                }

                // Check that if all the values in the matrix are either '.' or '1', print the easter egg
                bool allOne = true;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] != '.' && matrix[i, j] != '1')
                        {
                            allOne = false;
                            break;
                        }
                    }
                    if (!allOne) break;
                }
                if (allOne)
                {
                    Console.WriteLine($"All values in matrix are either '.' or '1' after {seconds} seconds.");
                    PrintMatrix(matrix);
                    break;
                }
                seconds++;
            }
            Console.WriteLine("Part 2 executed.");
        }

        public static void PrintMatrix(char[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == '.')
                    {
                        Console.Write("\x1B[32m" + matrix[i, j] + "\x1B[0m"); // green color for dots
                    }
                    else
                    {
                        Console.Write("\x1B[31m" + matrix[i, j] + "\x1B[0m"); // red color for other characters
                    }
                }
                Console.WriteLine();
            }
        }
    }
}