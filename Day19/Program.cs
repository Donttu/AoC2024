using System.Diagnostics;

namespace DayX
{
    class Program
    {
        static void Main(string[] args)
        {
            Solve.Execute(); // Runtime around 46ms
        }
    }
}

public class Solve
{
    /// <summary>
    /// Executes parts 1 and 2 of the day's puzzle.
    /// Reads the strings from strings.txt and substrings from subs.txt.
    /// Divides the strings into smaller chunks to be processed in parallel.
    /// Counts the number of composable strings and ways to match the pattern.
    /// Prints the results and execution time.
    /// </summary>
    public static void Execute()
    {
        // Read file strings.txt
        List<string> _strings = File.ReadAllLines("../../../strings.txt").ToList();
        List<string> _substrings = File.ReadAllText("../../../subs.txt").Split(',').ToList();
        _substrings = _substrings.Select(s => s.Trim()).ToList();

        // Divide the list into smaller chunks
        int chunkSize = 20; //  (400 / CPU threads)
        var chunks = Enumerable.Range(0, (int)Math.Ceiling((double)_strings.Count / chunkSize))
            .Select(i => _strings.Skip(i * chunkSize).Take(chunkSize).ToList())
            .ToList();

        // Count the number of composable strings and ways to match the pattern
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var results = chunks.AsParallel().Select(chunk => CountComposableStringsAndWays(chunk, _substrings)).ToList();
        stopwatch.Stop();
        var (count, ways) = results.Aggregate((a, b) => (a.Item1 + b.Item1, a.Item2 + b.Item2));

        // Print the results
        Console.WriteLine($"Part 1: {count}");
        Console.WriteLine($"Part 2: {ways}");
        Console.WriteLine($"Total time: {stopwatch.ElapsedMilliseconds}ms");
        Console.WriteLine("Part 1 and 2 executed.");
    }

    /// <summary>
    /// Counts the number of composable strings and ways to match the pattern.
    /// Iterates over the strings and uses CanComposeStringAndCountWays to check if each string can be composed
    /// and count the number of ways to match the pattern. Returns a tuple containing the count and total ways.
    /// </summary>
    public static (int, long) CountComposableStringsAndWays(List<string> _strings, List<string> _substrings)
    {
        int count = 0;
        long totalWays = 0;

        // Iterate over the strings
        foreach (var s in _strings)
        {
            // Count the number of composable strings and ways to match the pattern
            var (canCompose, ways) = CanComposeStringAndCountWays(s, _substrings, new Dictionary<string, (bool, long)>());
            if (canCompose)
            {
                count++;
                totalWays += ways;
            }
        }

        return (count, totalWays);
    }

    /// <summary>
    /// Checks if a given string can be composed from a list of substrings and counts the number of ways to match the pattern.
    /// Uses memoization to store the results of subproblems to avoid redundant computation.
    /// </summary>
    /// <param name="s">The string to check.</param>
    /// <param name="_substrings">The list of substrings.</param>
    /// <param name="memo">The memoization dictionary.</param>
    /// <returns>A tuple containing a boolean indicating if the string can be composed and the number of ways to match the pattern.</returns>
    private static (bool, long) CanComposeStringAndCountWays(string s, List<string> _substrings, Dictionary<string, (bool, long)> memo)
    {
        // Check if result is already memoized
        if (memo.TryGetValue(s, out var result))
        {
            return result;
        }

        // If the string is empty, it can be composed and there is one way to match the pattern
        if (string.IsNullOrEmpty(s))
        {
            return (true, 1);
        }

        long totalWays = 0;

        // Iterate over the substrings
        foreach (var substring in _substrings)
        {
            // Check if the string starts with the current substring
            if (s.StartsWith(substring))
            {
                // If so, recursively check the remaining part of the string
                var (canCompose, ways) = CanComposeStringAndCountWays(s.Substring(substring.Length), _substrings, memo);
                if (canCompose)
                {
                    totalWays += ways;
                }
            }
        }

        // Store the result in the memo dictionary
        memo[s] = (totalWays > 0, totalWays);

        return memo[s];
    }
}