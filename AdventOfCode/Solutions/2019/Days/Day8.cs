using System.Linq;
using System.Text;

namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 8, Name = "Space Image Format")]
    public class AoC2019Day8 : AoCSolution
    {
        private const int Width = 25;
        private const int Height = 6;

        private string[] layers;

        public override object RunPart1()
        {
            layers = new string[Input.Length / (Width * Height)];
            for (var i = 0; i < layers.Length; i++)
                layers[i] = Input[(Width * Height * i)..(Width * Height * (i + 1))];

            var l = layers.Aggregate((i, j) => i.Count(x => x == '0') < j.Count(x => x == '0') ? i : j);
            return l.Count(x => x == '1') * l.Count(x => x == '2');
        }

        public override object RunPart2()
        {
            return default;
        }
    }
}