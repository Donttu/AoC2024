using System;
using System.IO;

namespace Day08
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

        /// <summary>
        /// Prints a map with antinodes marked.
        /// A copy of the original map is created and the antinodes are marked on the copy.
        /// The map with antinodes is then printed to the console.
        /// </summary>
        /// <param name="map">The original map.</param>
        /// <param name="antinodes">The antinodes to mark on the map.</param>
        public static void Tulosta(char[,] map, HashSet<(int, int)> antinodes)
        {
            // Create a copy of the original map
            char[,] mapWithAntinodes = (char[,])map.Clone();

            // Mark antinodes on the copy of the map
            foreach (var (x, y) in antinodes)
            {
                mapWithAntinodes[x, y] = '#';
            }

            // Print the map with antinodes
            for (int i = 0; i < mapWithAntinodes.GetLength(0); i++)
            {
                for (int j = 0; j < mapWithAntinodes.GetLength(1); j++)
                {
                    Console.Write(mapWithAntinodes[i, j]);
                }
                Console.WriteLine();
            }
        }
    }

    public class Part1
    {
        /// <summary>
        /// Executes part 1 of the day's puzzle.
        /// Reads the input lines, converts them into a 2D character matrix.
        /// Identifies antinodes, which are calculated based on specific character occurrences within the matrix.
        /// Marks these antinodes on a copy of the original map and prints it.
        /// Also prints the total number of unique antinode locations found.
        /// </summary>
        /// <param name="lines">An array of strings, each containing a line of input.</param>
        public static void Execute(string[] lines)
        {
            // Create and "fill" matrix
            char[,] map = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    map[i, j] = lines[i][j];
                }
            }

            HashSet<(int, int)> antinodes = new HashSet<(int, int)>(); // Keep track of antinodes

            // Iterate over the map to find antinodes
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    char c = map[i, j]; // Current character

                    // Skip dots
                    if (c == '.') continue;

                    // Find all occurrences of the same character
                    List<(int, int)> occurrences = new List<(int, int)>(); // Keep track of specific occurrences
                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        for (int y = 0; y < map.GetLength(1); y++)
                        {
                            if (map[x, y] == c) occurrences.Add((x, y)); // Add occurrence of specific character
                        }
                    }

                    // Find antinodes by checking all pairs of occurrences
                    foreach (var (x1, y1) in occurrences)
                    {
                        foreach (var (x2, y2) in occurrences)
                        {
                            if (x1 == x2 && y1 == y2) continue; // Skip same occurrence

                            int dx = x2 - x1; // Distance between x coordinates
                            int dy = y2 - y1; // Distance between y coordinates

                            // Calculate the antinode position
                            int antinodeX = x2 + dx; // Antinode x coordinate
                            int antinodeY = y2 + dy; // Antinode y coordinate

                            // Check if antinode is within bounds
                            if (antinodeX >= 0 && antinodeX < map.GetLength(0) && antinodeY >= 0 && antinodeY < map.GetLength(1))
                            {
                                antinodes.Add((antinodeX, antinodeY));
                            }

                            // Calculate the other antinode position
                            antinodeX = x1 - dx; // Antinode x coordinate
                            antinodeY = y1 - dy; // Antinode y coordinate

                            // Check if antinode is within bounds
                            if (antinodeX >= 0 && antinodeX < map.GetLength(0) && antinodeY >= 0 && antinodeY < map.GetLength(1))
                            {
                                antinodes.Add((antinodeX, antinodeY));
                            }
                        }
                    }
                }
            }

            Program.Tulosta(map, antinodes); // Print the map with antinodes          
            Console.WriteLine($"Number of unique antinode locations: {antinodes.Count}"); // Print the count of unique antinode locations
            Console.WriteLine("Part 1 executed.");
        }
    }

    public class Part2
    {
        /// <summary>
        /// Executes part 2 of the day's puzzle.
        /// Creates a 2D matrix from the input and counts the number of unique antinode locations.
        /// Prints the count of unique antinode locations.
        /// </summary>
        /// <param name="lines">An array of strings, each containing a line of input.</param>
        public static void Execute(string[] lines)
        {
            // Create and "fill" matrix
            char[,] map = new char[lines.Length, lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    map[i, j] = lines[i][j];
                }
            }

            HashSet<(int, int)> antinodes = new HashSet<(int, int)>(); // Keep track of antinodes

            // Iterate over the map to find antinodes
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    char c = map[i, j];

                    // Skip dots
                    if (c == '.') continue;

                    // Find all occurrences of the same character
                    List<(int, int)> occurrences = new List<(int, int)>(); // Keep track of specificoccurrences
                    for (int x = 0; x < map.GetLength(0); x++)
                    {
                        for (int y = 0; y < map.GetLength(1); y++)
                        {
                            if (map[x, y] == c) occurrences.Add((x, y)); // Add occurrence of specific character
                        }
                    }

                    // Check if this position is an antinode
                    if (occurrences.Count > 1)
                    {
                        antinodes.Add((i, j));
                    }

                    // Find other antinodes in the same line
                    foreach (var (x1, y1) in occurrences)
                    {
                        foreach (var (x2, y2) in occurrences)
                        {
                            if (x1 == x2 && y1 == y2) continue; // Skip same occurrence

                            int dx = x2 - x1; // Distance between x coordinates
                            int dy = y2 - y1; // Distance between y coordinates

                            // Calculate the antinode position
                            for (int k = -map.GetLength(0); k <= map.GetLength(0); k++) // Iterate over all possible antinode positions
                            {
                                int antinodeX = x1 + k * dx; // Antinode x coordinate
                                int antinodeY = y1 + k * dy; // Antinode y coordinate

                                // Check if antinode is within bounds
                                if (antinodeX >= 0 && antinodeX < map.GetLength(0) && antinodeY >= 0 && antinodeY < map.GetLength(1))
                                {
                                    antinodes.Add((antinodeX, antinodeY));
                                }
                            }
                        }
                    }
                }
            }

            Program.Tulosta(map, antinodes); // Print the map with antinodes            
            Console.WriteLine($"Number of unique antinode locations: {antinodes.Count}"); // Print the count of unique antinode locations
            Console.WriteLine("Part 2 executed.");
        }
    }
}