using System;
using System.IO;

namespace Day12
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
            char[,] matrix = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                }
            }

            List<Region> regions = new List<Region>();
            bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!visited[i, j])
                    {
                        Region region = new Region(matrix[i, j]);
                        DFS(matrix, visited, region, i, j);
                        regions.Add(region);
                    }
                }
            }

            foreach (var region in regions)
            {
                Console.WriteLine($"Region type: {region.Type}, Coordinates: {string.Join(", ", region.Coordinates)}");
            }

            int[,] boundaryCounts = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // Check neighbors
                    if (i > 0 && matrix[i, j] != matrix[i - 1, j]) // Up
                    {
                        boundaryCounts[i, j]++;
                    }
                    if (i < matrix.GetLength(0) - 1 && matrix[i, j] != matrix[i + 1, j]) // Down
                    {
                        boundaryCounts[i, j]++;
                    }
                    if (j > 0 && matrix[i, j] != matrix[i, j - 1]) // Left
                    {
                        boundaryCounts[i, j]++;
                    }
                    if (j < matrix.GetLength(1) - 1 && matrix[i, j] != matrix[i, j + 1]) // Right
                    {
                        boundaryCounts[i, j]++;
                    }

                    // Check edges
                    if (i == 0) // Top edge
                    {
                        boundaryCounts[i, j]++;
                    }
                    if (i == matrix.GetLength(0) - 1) // Bottom edge
                    {
                        boundaryCounts[i, j]++;
                    }
                    if (j == 0) // Left edge
                    {
                        boundaryCounts[i, j]++;
                    }
                    if (j == matrix.GetLength(1) - 1) // Right edge
                    {
                        boundaryCounts[i, j]++;
                    }
                }
            }

            foreach (var region in regions)
            {
                foreach (var (x, y) in region.Coordinates)
                {
                    region.BoundaryCount += boundaryCounts[x, y];
                    region.Size++;
                }
            }

            int totalSum = 0;
            foreach (var region in regions)
            {
                int result = region.Size * region.BoundaryCount;
                Console.WriteLine($"Region type: {region.Type}, Size multiplied by boundary count: {result}");
                totalSum += result;
            }

            Console.WriteLine($"Total sum: {totalSum}");
            Console.WriteLine("Part 1 executed.");
        }

        private static void DFS(char[,] matrix, bool[,] visited, Region region, int i, int j)
        {
            if (i < 0 || i >= matrix.GetLength(0) || j < 0 || j >= matrix.GetLength(1) || visited[i, j] || matrix[i, j] != region.Type)
            {
                return;
            }

            visited[i, j] = true;
            region.Coordinates.Add((i, j));

            DFS(matrix, visited, region, i - 1, j); // Up
            DFS(matrix, visited, region, i + 1, j); // Down
            DFS(matrix, visited, region, i, j - 1); // Left
            DFS(matrix, visited, region, i, j + 1); // Right
        }

        private class Region
        {
            public List<(int, int)> Coordinates { get; set; }
            public char Type { get; set; }
            public int BoundaryCount { get; set; }
            public int Size { get; set; }

            public Region(char type)
            {
                Type = type;
                Coordinates = new List<(int, int)>();
                BoundaryCount = 0;
                Size = 0;
            }
        }
    }


    // Part 2 - Part 2 - Part 2 - Part 2 - Part 2 - Part 2 - Part 2 - Part 2 - Part 2 - Part 2 - 


    public class Part2
    {
        public static void Execute(string[] lines)
        {
            char[,] matrix = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i, j] = lines[i][j];
                }
            }

            List<Region> regions = new List<Region>();
            bool[,] visited = new bool[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++) // Rows
            {
                for (int j = 0; j < matrix.GetLength(1); j++) // Columns
                {
                    if (!visited[i, j])
                    {
                        Region region = new Region(matrix[i, j]);
                        DFS(matrix, visited, region, i, j);
                        regions.Add(region);
                    }
                }
            }

            foreach (var region in regions)
            {
                region.Size = region.Coordinates.Count;
                Console.WriteLine($"Region type: {region.Type}, Size: {region.Size}, Count of Coordinates: {region.Coordinates.Count}, Cell count: {region.Cells.Count}");
            }


            foreach (var region in regions)
            {
                Console.WriteLine($"Handling region: {region.Type}");
                if (region.Cells.Count == 1)
                {
                    region.SideCount = 4;
                }
                else
                {
                    // Count the corners of the perimeter
                    int cornerCount = 0;

                    foreach (var cell in region.Cells)
                    {
                        if (CountNeighbours(matrix, region, cell) == 1)
                        {
                            cornerCount += 2;
                            continue;
                        }
                        else if (CountNeighbours(matrix, region, cell) == 2)
                        {
                            if (NeighboursInLine(matrix, region, cell))
                            {
                                continue;
                            }
                            cornerCount++;
                            if (HasInsideCorner(matrix, region, cell))
                            {
                                cornerCount++;
                            }
                            continue;
                        }
                        else if (CountNeighbours(matrix, region, cell) == 3)
                        {
                            // Get the three neighbours of "cell"
                            var neighbours = region.Cells.Where(n => Math.Abs(n.R - cell.R) + Math.Abs(n.C - cell.C) == 1).ToList();

                            // Take all combinations of two neighbour cells that are not in line with "cell"
                            if (neighbours[0].R != neighbours[1].R && neighbours[0].C != neighbours[1].C)
                            {
                                int r4 = cell.R ^ neighbours[0].R ^ neighbours[1].R;
                                int c4 = cell.C ^ neighbours[0].C ^ neighbours[1].C;
                                if (matrix[r4, c4] != cell.Type)
                                {
                                    cornerCount++;
                                }
                            }
                            if (neighbours[0].R != neighbours[2].R && neighbours[0].C != neighbours[2].C)
                            {
                                int r4 = cell.R ^ neighbours[0].R ^ neighbours[2].R;
                                int c4 = cell.C ^ neighbours[0].C ^ neighbours[2].C;
                                if (matrix[r4, c4] != cell.Type)
                                {
                                    cornerCount++;
                                }
                            }
                            if (neighbours[1].R != neighbours[2].R && neighbours[1].C != neighbours[2].C)
                            {
                                int r4 = cell.R ^ neighbours[1].R ^ neighbours[2].R;
                                int c4 = cell.C ^ neighbours[1].C ^ neighbours[2].C;
                                if (matrix[r4, c4] != cell.Type)
                                {
                                    cornerCount++;
                                }
                            }
                            continue;
                        }
                        else if (CountNeighbours(matrix, region, cell) == 4)
                        {
                            // Check the four cells that are diagonally one step away from "cell". 
                            // For each of these four cells, execute cornerCount++ if the type of the cell is not equal to the type of "cell"

                            for (int r = -1; r <= 1; r += 2)
                            {
                                for (int c = -1; c <= 1; c += 2)
                                {
                                    int r3 = cell.R + r;
                                    int c3 = cell.C + c;
                                    if (r3 >= 0 && r3 < matrix.GetLength(0) && c3 >= 0 && c3 < matrix.GetLength(1) && matrix[r3, c3] != matrix[cell.R, cell.C])
                                    {
                                        cornerCount++;
                                    }
                                }
                            }
                        }
                    }
                    region.SideCount = cornerCount;
                }
            }

            int totalPrice = 0;
            foreach (var region in regions)
            {
                int result = region.Size * region.SideCount;
                Console.WriteLine($"Region type: {region.Type}, Size: {region.Size} multiplied by side count {region.SideCount}: {result}");
                totalPrice += result;
            }

            Console.WriteLine("Total price: " + totalPrice);
            Console.WriteLine("Part 2 executed.");
        }


        private static bool HasInsideCorner(char[,] matrix, Region region, Cell cell)
        {
            // Get the two neighbours of "cell"
            var neighbours = region.Cells.Where(n => Math.Abs(n.R - cell.R) + Math.Abs(n.C - cell.C) == 1).ToList();

            int r1 = cell.R, c1 = cell.C, r2 = neighbours[0].R, c2 = neighbours[0].C, r3 = neighbours[1].R, c3 = neighbours[1].C;
            int r4 = r1 ^ r2 ^ r3, c4 = c1 ^ c2 ^ c3;

            // Return false, if the fourth cell has the same type as "cell"
            return matrix[r4, c4] != cell.Type;
        }

        private static bool NeighboursInLine(char[,] matrix, Region region, Cell cell)
        {
            // Return true, if the two neighbours of "cell" are either in the same row or column as "cell"
            var neighbours = region.Cells.Where(n => Math.Abs(n.R - cell.R) + Math.Abs(n.C - cell.C) == 1).ToList();
            return neighbours.Count(n => n.R == cell.R) == 2 || neighbours.Count(n => n.C == cell.C) == 2;
        }

        private static int CountNeighbours(char[,] matrix, Region region, Cell cell)
        {
            int count = 0;
            foreach (var neighbour in region.Cells)
            {
                if (Math.Abs(neighbour.R - cell.R) + Math.Abs(neighbour.C - cell.C) == 1)
                {
                    count++;
                }
            }
            return count;
        }

        private static void DFS(char[,] matrix, bool[,] visited, Region region, int i, int j)
        {
            if (i < 0 || i >= matrix.GetLength(0) || j < 0 || j >= matrix.GetLength(1) || visited[i, j] || matrix[i, j] != region.Type)
            {
                return;
            }

            visited[i, j] = true;
            region.Coordinates.Add((i, j));
            Cell cell = new Cell(matrix[i, j], i, j);
            region.Cells.Add(cell);

            DFS(matrix, visited, region, i - 1, j); // Up
            DFS(matrix, visited, region, i + 1, j); // Down
            DFS(matrix, visited, region, i, j - 1); // Left
            DFS(matrix, visited, region, i, j + 1); // Right
        }

        private class Region
        {
            public List<Cell> Cells { get; set; }
            public char Type { get; set; }
            public int Size { get; set; }
            public List<(int, int)> Coordinates { get; set; }
            public int BoundaryCount { get; set; }
            public int SideCount { get; set; }

            public Region(char type)
            {
                Type = type;
                Coordinates = new List<(int, int)>();
                Size = 0;
                Cells = new List<Cell>();
                BoundaryCount = 0;
            }
        }

        private class Cell
        {
            public char Type { get; set; }
            public int R { get; set; }
            public int C { get; set; }
            public Cell(char type, int x, int y)
            {
                R = x;
                C = y;
                Type = type;
            }
        }

    }
}