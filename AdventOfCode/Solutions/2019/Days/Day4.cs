using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 4, Name = "Secure Container")]
    public class AoC2019Day4 : AoCSolution
    {
        public override int RunPart1()
        {
            var inp = Input.Split('-').Select(int.Parse).ToArray();
            int min = inp[0], max = inp[1];
            var range = Enumerable.Range(min, max - min) //
                .Where(x => 99999 < x && x <= 999999) //
                .Where(FilterNoDecreasing) //
                .Where(RequireAnyTwoAdjacentDigits);
            return range.Count();
        }

        public override int RunPart2()
        {
            var inp = Input.Split('-').Select(int.Parse).ToArray();
            int min = inp[0], max = inp[1];
            var range = Enumerable.Range(min, max - min) //
                .Where(x => 99999 < x && x <= 999999) //
                .Where(FilterNoDecreasing) //
                .Where(RequireExactlyTwoAdjacentDigits);
            return range.Count();
        }

        private static bool FilterNoDecreasing(int x)
        {
            var chars = x.ToString().ToCharArray();
            for (var i = 1; i < chars.Length; i++)
                if (chars[i] < chars[i - 1])
                    return false;
            return true;
        }

        private static bool RequireAnyTwoAdjacentDigits(int x)
        {
            var str = x.ToString();
            return str.Any(y => str.LastIndexOf(y) - str.IndexOf(y) > 0);
        }

        private static bool RequireExactlyTwoAdjacentDigits(int x)
        {
            var str = x.ToString();
            return str.Any(y => str.LastIndexOf(y) - str.IndexOf(y) == 1);
        }
    }
}