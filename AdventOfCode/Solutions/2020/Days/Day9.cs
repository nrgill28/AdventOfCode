using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 9, Name = "Encoding Error")]
    public sealed class AoC2020Day9 : AoCSolution
    {
        private const int PreambleLength = 25;
        private long _part1;

        public override object RunPart1()
        {
            var list = new List<long>();
            foreach (var num in InputAsLongs())
            {
                if (list.Count < PreambleLength)
                {
                    list.Add(num);
                    continue;
                }

                if (!DoesListHaveSum(list, num))
                {
                    _part1 = num;
                    return num;
                }

                list.RemoveAt(0);
                list.Add(num);
            }

            return -1;
        }

        public override object RunPart2()
        {
            var input = InputAsLongs();
            for (var i = 0; i < input.Length - 1; i++)
            {
                var sum = input[i];
                for (var j = i + 1; j < input.Length; j++)
                {
                    sum += input[j];
                    if (sum == _part1)
                    {
                        var range = input.Skip(i).Take(j - i).ToArray();
                        return range.Min() + range.Max();
                    } 
                    if (sum > _part1) break;
                }
            }

            return -1;
        }

        private static bool DoesListHaveSum(IList<long> list, long sum)
        {
            for (var i = 0; i < list.Count - 1; i++)
            for (var j = i + 1; j < list.Count; j++)
                if (list[i] + list[j] == sum)
                    return true;
            return false;
        }
    }
}