namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 2, Name = "Gravity Assist")]
    public class AoC2019Day2 : AoCSolution
    {
        public override int RunPart1()
        {
            var computer = new IntcodeComputer(InputAsInts(','));
            computer.SetMemoryValueAt(1, 12);
            computer.SetMemoryValueAt(2, 2);
            computer.RunUntilHalt();
            return computer.GetMemoryValueAt(0);
        }

        public override int RunPart2()
        {
            var computer = new IntcodeComputer(InputAsInts(','));
            for (int noun = 0; noun < 100; noun++)
            for (int verb = 0; verb < 100; verb++)
            {
                computer.Reset();
                computer.SetMemoryValueAt(1, noun);
                computer.SetMemoryValueAt(2, verb);
                computer.RunUntilHalt();

                if (computer.GetMemoryValueAt(0) == 19690720)
                    return 100 * noun + verb;
            }

            return default;
        }
    }
}