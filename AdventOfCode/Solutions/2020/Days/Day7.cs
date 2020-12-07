using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 7, Name = "Handy Haversacks")]
    public sealed class AoC2020Day7 : AoCSolution
    {
        private readonly Dictionary<string, List<Tuple<int, string>>> _bags = new();

        public override void Setup()
        {
            // For each bag rule in the input
            foreach (var line in InputAsLines)
            {
                // Get the name of the bag this rule applies to
                var bagName = line.Split(" bags contain ")[0];

                // Normalize the rules
                var foo = line.Split(" bags contain ")[1].Replace(".", "");
                
                // Magic LINQ to convert the rules into a List<Tuple<int, string>>
                _bags.Add(bagName, (
                    from contains in foo.Split(", ")
                    where contains != "no other bags"
                    select contains.Replace(" bags", "").Replace(" bag", "")
                    into bar
                    let count = int.Parse(bar.Split(" ")[0])
                    let name = bar.Substring(bar.IndexOf(" ", StringComparison.Ordinal)).Trim()
                    select new Tuple<int, string>(count, name)
                ).ToList());
            }
        }

        public override object RunPart1()
        {
            // Just count how many bags that are able to hold a shiny gold bag
            return _bags.Count(x => CanHold(x.Value, "shiny gold"));
        }

        public override object RunPart2()
        {
            // Return the total number of children in the shiny gold bag
            return TotalChildren("shiny gold");
        }

        private bool CanHold(IEnumerable<Tuple<int, string>> bags, string target)
        {
            // Foreach child in the given bag
            foreach (var (_, bag) in bags)
            {
                // If it is the target, return true
                if (bag == target) return true;
                // Or if it can hold the target return true
                if (CanHold(_bags[bag], target)) return true;
            }

            // If not, return false
            return false;
        }

        private int TotalChildren(string bag)
        {
            return _bags[bag].Aggregate(0, (sum, contents) => sum + contents.Item1 + contents.Item1 * TotalChildren(contents.Item2));
        }
    }
}