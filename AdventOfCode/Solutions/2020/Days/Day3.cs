namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 3, Name = "Toboggan Trajectory")]
    public sealed class AoC2020Day3 : AoCSolution
    {
        private bool[,] _hill;

        //protected override string DebugInput => "..##.......\n#...#...#..\n.#....#..#.\n..#.#...#.#\n.#...##..#.\n..#.##.....\n.#.#.#....#\n.#........#\n#.##...#...\n#...##....#\n.#..#...#.#";

        public override void Setup()
        {
            var lines = InputAsLines;
            _hill = new bool[lines[0].Length, lines.Length];

            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (var x = 0; x < line.Length; x++)
                {
                    var c = line[x];
                    _hill[x, y] = c == '#';
                }
            }
        }

        private int CountTreesOnSlope(int xStep, int yStep)
        {
            var count = 0;
            for (var y = 0; y < _hill.GetLength(1); y += yStep)
            {
                var x = xStep * (y / yStep) % _hill.GetLength(0);
                if (_hill[x, y]) count++;
            }

            return count;
        }

        public override object RunPart1()
        {
            return CountTreesOnSlope(3, 1);
        }

        public override object RunPart2()
        {
            var total = (long) CountTreesOnSlope(1, 1);
            total *= CountTreesOnSlope(3, 1);
            total *= CountTreesOnSlope(5, 1);
            total *= CountTreesOnSlope(7, 1);
            total *= CountTreesOnSlope(1, 2);
            return total;
        }
    }
}