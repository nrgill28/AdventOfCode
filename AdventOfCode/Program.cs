using System.Collections.Generic;
using System.Linq;
using Colorful;

namespace AdventOfCode
{
    internal static class Program
    {
        private static readonly Dictionary<int, AoCSolution[]> Days = new();
        
        private static void Main()
        {
            // Grab all the days
            Init();
            
            // TODO: Better menu
            Console.Write("Input a year / day to run (YYYY/DD): ");
            var inp = Console.ReadLine().Split('/');
            
            if (!int.TryParse(inp[0], out var year) || !int.TryParse(inp[1], out var day))
                return;

            if (!Days.ContainsKey(year) || Days[year][day] == null)
                return;

            var result = RunSolution(Days[year][day]);
            
            Console.WriteLine($"Part 1: {result.Item1}\nPart 2: {result.Item2}");
            
        }

        private static void Init()
        {
            // Get all the days
            var days = AoCSolution.GetDays().ToArray();

            // For all the days, insert them into the dictionary
            foreach (var day in days)
            {
                var attrib = day.Attribute;
                if (!Days.ContainsKey(attrib.Year))
                    Days.Add(attrib.Year, new AoCSolution[24]);
                Days[attrib.Year][attrib.Day] = day;
            }
        }

        private static (object, object) RunSolution(AoCSolution solution)
        {
            solution.Init();
            return (solution.RunPart1(), solution.RunPart2());
        }
    }
}