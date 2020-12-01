using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using AdventOfCode2020.Days;
using Colorful;

namespace AdventOfCode2020
{
    internal static class Program
    {
        private const string InputPath = "Inputs/input_day{0}.txt";

        private static readonly Dictionary<string, AoCDay> Days = new()
        {
            {
                "Report Repair", new Day1()
            }
        };

        private static void Main(string[] args)
        {
            // Draw the menu
            DrawMenu();

            while (true)
            {
                // Prompt the user to select a day
                Console.Write("Select a day: ");
                var inp = Console.ReadLine();

                // Check the input is an integer
                if (!int.TryParse(inp, out var selection))
                {
                    Console.WriteLine("Please input an integer", Color.Red);
                    continue;
                }

                // Check the input is in range
                if (selection < 1 || selection > Days.Count)
                {
                    Console.WriteLine($"Selection must be between 1 and {Days.Count}");
                    continue;
                }

                // Get and set the input text
                var inputPath = string.Format(InputPath, selection);
                var day = Days.Values.ElementAt(selection - 1);
                day.Input = File.ReadAllText(inputPath);

                // Get the results for parts 1 and 2
                var sw = Stopwatch.StartNew();
                var resultPart1 = day.RunPart1();
                var durationPart1 = sw.Elapsed;
                var resultPart2 = day.RunPart2();
                var durationPart2 = sw.Elapsed;

                // Redraw the menu
                DrawMenu();
                Console.WriteLine($"Part 1 ({durationPart1.TotalMilliseconds}ms): {resultPart1}");
                Console.WriteLine($"Part 2 ({durationPart2.TotalMilliseconds}ms): {resultPart2}");
            }
        }

        private static void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine("Advent of Code 2020\n---------------------");
            var counter = 0;
            foreach (var day in Days)
            {
                Console.WriteLine($"Day {counter + 1}: {day.Key}", counter % 2 == 0 ? Color.Green : Color.Red);
                counter++;
            }

            Console.WriteLine("---------------------\n");
        }
    }
}