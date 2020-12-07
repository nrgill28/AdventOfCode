using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 6, Name = "Universal Orbit Map")]
    public sealed class AoC2019Day6 : AoCSolution
    {
        private readonly Dictionary<string, string> _map = new(); 
        
        public override object RunPart1()
        {
            // Build the map
            _map.Add("COM", null);
            foreach (var relation in InputAsLines)
            {
                var split = relation.Split(')');
                _map.Add(split[1], split[0]);
            }
            
            // Now do the iteration
            return _map.Keys.Sum(CountOrbits);
        }

        public override object RunPart2()
        {
            // Get the steps required for me to get to COM
            List<string> youToCom = new();
            var current = "YOU";
            while (current != "COM")
            {
                current = _map[current];
                youToCom.Add(current);
            }
            
            // Now start doing the same for SAN, except we don't need to record which steps we take, only the number
            var steps = -1;
            current = "SAN";
            while (!youToCom.Contains(current))
            {
                current = _map[current];
                steps++;
            }

            return youToCom.IndexOf(current) + steps;
        }

        private int CountOrbits(string start)
        {
            if (_map[start] == null)
                return 0;
            return CountOrbits(_map[start]) + 1;
        }
    }
}