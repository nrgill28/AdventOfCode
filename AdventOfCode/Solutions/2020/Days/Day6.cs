using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 6, Name = "Custom Customs")]
    public sealed class AoC2020Day6 : AoCSolution
    {
        //protected override string DebugInput => "abc\n\na\nb\nc\n\nab\nac\n\na\na\na\na\n\nb";

        public override object RunPart1()
        {
            return Input.Split("\n\n").Aggregate(0, (sum, group) =>
            {
                var questions = new bool[26];
                
                foreach (var person in group.Split("\n").Where(l => !string.IsNullOrWhiteSpace(l)))
                foreach (var answer in person)
                    questions[answer - 97] = true;
                
                return sum + questions.Count(x => x);
            });
        }

        public override object RunPart2()
        {
            return Input.Split("\n\n").Aggregate(0, (sum, group) =>
            {
                var questions = new int[26];
                var people = group.Split("\n").Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
                
                foreach (var person in people)
                foreach (var answer in person)
                    questions[answer - 97] += 1;
                
                return sum + questions.Count(x => x == people.Length);
            });
        }
    }
}