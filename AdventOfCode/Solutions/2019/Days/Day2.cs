namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 2, Name = "Gravity Assist")]
    public class AoC2019Day2 : AoCSolution
    {
        public override object RunPart1()
        {
            var computer = new IntcodeComputer(InputAsIntcode);
            computer.SetMemoryValue(2, 2);
            computer.RunUntilHalt();
            return computer.GetMemoryValue(0);
        }

        public override object RunPart2()
        {
            var computer = new IntcodeComputer(InputAsIntcode);
            for (int noun = 0; noun < 100; noun++)
            for (int verb = 0; verb < 100; verb++)
            {
                computer.Reset();
                computer.SetMemoryValue(1, noun);
                computer.SetMemoryValue(2, verb);
                computer.RunUntilHalt();

                if (computer.GetMemoryValue(0) == 19690720)
                    return 100 * noun + verb;
            }

            return default;
        }
    }
}