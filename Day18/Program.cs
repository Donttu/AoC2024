using System;
using System.IO;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the input (separated by newline):");

            string input = string.Empty;
            string line;
            while ((line = Console.ReadLine() ?? string.Empty) != string.Empty)
            {
                input += line + Environment.NewLine;
            }

            string[] lines = input.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Solve.Execute(lines);
        }
    }

    public class Solve
    {
        public static void Execute(string[] lines)
        {
            char[,] matrix = new char[71, 71];
            for (int i = 0; i <= 70; i++)
            {
                for (int j = 0; j <= 70; j++)
                {
                    matrix[i, j] = '.';
                }
            }
            for (int i = 0; i < 1024; i++)
            {
                matrix[int.Parse(lines[i].Split(",")[1]), int.Parse(lines[i].Split(",")[0])] = '#';
            }


            Traverse(matrix, (0, 0), (70, 70));
            Console.WriteLine("Part 1 executed.");

            for (int i = 1024; i < lines.Length; i++)
            {
                matrix[int.Parse(lines[i].Split(",")[1]), int.Parse(lines[i].Split(",")[0])] = '#';
                if (Traverse(matrix, (0, 0), (70, 70)) == -1)
                {
                    Console.WriteLine("Byte (" + lines[i].Split(",")[0] + ", " + lines[i].Split(",")[1] + ") blocked the path.");
                    break;
                }
            }
        }

        public static int Traverse(char[,] matrix, (int, int) start, (int, int) end)
        {
            var queue = new Queue<(int, int)>();
            var visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            var steps = new int[matrix.GetLength(0), matrix.GetLength(1)];

            queue.Enqueue(start);
            visited[start.Item1, start.Item2] = true;
            steps[start.Item1, start.Item2] = 0;

            var directions = new (int, int)[] { (0, 1), (0, -1), (1, 0), (-1, 0) };

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == end)
                {
                    //Console.WriteLine($"Found path from {start} to {end} in {steps[end.Item1, end.Item2]} steps");
                    return steps[end.Item1, end.Item2];
                }

                foreach (var (dx, dy) in directions)
                {
                    var (newX, newY) = (current.Item1 + dx, current.Item2 + dy);

                    if (newX >= 0 && newX < matrix.GetLength(0) && newY >= 0 && newY < matrix.GetLength(1) && matrix[newX, newY] == '.' && !visited[newX, newY])
                    {
                        queue.Enqueue((newX, newY));
                        visited[newX, newY] = true;
                        steps[newX, newY] = steps[current.Item1, current.Item2] + 1;
                    }
                }
            }
            //Console.WriteLine($"No path found from {start} to {end}");
            return -1;
        }
    }
}