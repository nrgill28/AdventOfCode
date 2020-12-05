using System;
using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 5, Name = "Binary Boarding")]
    public class AoC2020Day5 : AoCSolution
    {
        public override object RunPart1()
        {
            var max = int.MinValue;
            foreach (var line in InputAsLines)
            {
                var row = Convert.ToInt32(string.Concat(line.Take(7).Select(x => x == 'F' ? '0' : '1')), 2);
                var column = Convert.ToInt32(string.Concat(line.Skip(7).Take(3).Select(x => x == 'R' ? '1' : '0')), 2);

                var id = row * 8 + column;
                if (id > max) max = id;
            }

            return max;
        }

        public override object RunPart2()
        {
            var taken = new bool[128 * 8];
            
            foreach (var line in InputAsLines)
            {
                var row = Convert.ToInt32(string.Concat(line.Take(7).Select(x => x == 'F' ? '0' : '1')), 2);
                var column = Convert.ToInt32(string.Concat(line.Skip(7).Take(3).Select(x => x == 'R' ? '1' : '0')), 2);

                taken[row * 8 + column] = true;
            }

            int myIndex = 0;
            for (int i = 0; i < taken.Length; i++)
            {
                if (i == 0 || i == taken.Length - 1 || taken[i]) continue;
                if (!taken[i - 1] || !taken[i + 1]) continue;
                myIndex = i;
            }

            return myIndex;
        }
    }
}