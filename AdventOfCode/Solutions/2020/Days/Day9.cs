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
            // Declare a new list
            var list = new List<long>();
            
            // Start iterating over the numbers in the input
            foreach (var num in InputAsLongs())
            {
                // If we haven't yet filled our list add the number and skip to the next iteration
                if (list.Count < PreambleLength)
                {
                    list.Add(num);
                    continue;
                }

                // If the list doesn't contain to distinct numbers that sum to the current number
                // we found our value. Return it
                if (!DoesListHaveSum(list, num))
                {
                    _part1 = num;
                    return num;
                }

                // Otherwise remove the first element and add the current number
                list.RemoveAt(0);
                list.Add(num);
            }

            return -1;
        }

        public override object RunPart2()
        {
            // Get our input
            var input = InputAsLongs();
            
            // For each number in the input
            for (var i = 0; i < input.Length - 1; i++)
            {
                // Start summing the numbers that come directly after it
                var sum = input[i];
                for (var j = i + 1; j < input.Length; j++)
                {
                    sum += input[j];
                    
                    // If we reached the target value;
                    if (sum == _part1)
                    {
                        // Get the range and return the min plus the max
                        var range = input.Skip(i).Take(j - i).ToArray();
                        return range.Min() + range.Max();
                    } 
                    
                    // If we overshot the target value skip to the next loop
                    if (sum > _part1) break;
                }
            }

            return -1;
        }

        private static bool DoesListHaveSum(IList<long> list, long sum)
        {
            // Checks if two distinct numbers in the given list sum to the given value
            for (var i = 0; i < list.Count - 1; i++)
            for (var j = i + 1; j < list.Count; j++)
                if (list[i] + list[j] == sum)
                    return true;
            return false;
        }
    }
}