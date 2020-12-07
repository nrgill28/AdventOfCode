namespace AdventOfCode
{
    /// <summary>
    ///     Advent of Code Day 1
    ///     https://adventofcode.com/2020/day/1
    /// </summary>
    [AoCSolution(Day = 1, Year = 2020, Name = "Report Repair")]
    public sealed class AoC2020Day1 : AoCSolution
    {
        public override object RunPart1()
        {
            // Get the input as a sequence of integers
            var ints = InputAsInts();

            // Check each pair of numbers
            for (var i = 0; i < ints.Length; i++)
            for (var j = i; j < ints.Length; j++)
                // If the sum of the two numbers equals 2020, return their product
                if (ints[i] + ints[j] == 2020)
                    return ints[i] * ints[j];

            // Shouldn't happen, but return default if something went wrong. (Bad input?)
            return default;
        }

        public override object RunPart2()
        {
            // Get the input as a sequence of integers
            var ints = InputAsInts();

            // Stack 3 loops this time
            for (var i = 0; i < ints.Length; i++)
            for (var j = i; j < ints.Length; j++)
            for (var k = j; k < ints.Length; k++)
                // If the sum of the three numbers equals 2020, return their product
                if (ints[i] + ints[j] + ints[k] == 2020)
                    return ints[i] * ints[j] * ints[k];

            // Shouldn't happen, but return default if something went wrong. (Bad input?)
            return default;
        }
    }
}