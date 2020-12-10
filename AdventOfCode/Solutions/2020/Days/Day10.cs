using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 10, Name = "Adapter Array")]
    public class Day10 : AoCSolution
    {
        public override object RunPart1()
        {
            // Initialize some vars
            int[] jumps = {0, 0, 0, 1};
            var input = InputAsInts().OrderBy(x => x);

            // Iterate over each value in the sorted array
            var current = 0;
            foreach (var num in input)
            {
                // Record what size of difference the two values had
                jumps[num - current]++;
                current = num;
            }

            // Return the solution
            return jumps[1] * jumps[3];
        }

        public override object RunPart2()
        {
            // Get the input as an array of sorted longs
            var input = InputAsLongs().OrderBy(x => x).ToArray();
            // Make an array of longs the same length as the input initialized to -1
            var counts = Enumerable.Repeat(-1L, input.Length).ToArray();
            // Find the max value in our input
            var target = input.Max();
            
            // Count the number of ways we can reach the target value
            return CountPathsToTarget(in input, in counts, -1, target);;
        }

        private static long CountPathsToTarget(in long[] sequence, in long[] counts, int start, long target)
        {
            // If we've already calculated the number of paths from this position, return it
            if (start != -1 && counts[start] != -1)
                return counts[start];

            // Otherwise declare some vars
            var sum = 0L;
            var x = start == -1 ? 0 : sequence[start];
            
            // Start a loop at our current number + 1
            for (var i = start + 1; i < sequence.Length; i++)
            {
                // Continue the loop until the numbers are out of range
                if (sequence[i] - x > 3) break;
                
                // Or until we hit the target number
                if (sequence[i] == target)
                {
                    sum++;
                    break;
                }

                // Recursively sum
                sum += CountPathsToTarget(in sequence, in counts, i, target);
            }
            
            // If this isn't the original method call remember the sum we reached
            if (start != -1)
                counts[start] = sum;
            
            // Then return it
            return sum;
        }
    }
}