using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 2, Name = "Password Philosophy")]
    public class Day2 : AoCSolution
    {
        private const string Pattern = @"^(\d*)-(\d*) (.): (.*)$";
        
        public override object RunPart1()
        {
            return InputAsLines.Count(x =>
            {
                var result = Regex.Match(x, Pattern);
                var min = int.Parse(result.Groups[1].Value);
                var max = int.Parse(result.Groups[2].Value);
                var c = result.Groups[3].Value[0];
                var password = result.Groups[4].Value;
                var count = password.Count(x => x == c);

                return count >= min && count <= max;
            });
        }

        public override object RunPart2()
        {
            return InputAsLines.Count(x =>
            {
                var result = Regex.Match(x, Pattern);
                var i1 = int.Parse(result.Groups[1].Value);
                var i2 = int.Parse(result.Groups[2].Value);
                var c = result.Groups[3].Value[0];
                var password = result.Groups[4].Value;

                return password[i1 - 1] == c ^ password[i2 - 1] == c;
            });
        }
    }
}