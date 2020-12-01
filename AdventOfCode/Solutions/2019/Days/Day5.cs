﻿using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 5, Name = "Sunny with a Chance of Asteroids")]
    public class AoC2019Day5 : AoCSolution
    {
        public override int RunPart1()
        {
            var computer = new IntcodeComputer(InputAsInts(','));
            computer.Input.Enqueue(1);
            computer.RunUntilHalt();
            return computer.Output.Last();
        }

        public override int RunPart2()
        {
            var computer = new IntcodeComputer(InputAsInts(','));
            computer.Input.Enqueue(5);
            computer.RunUntilHalt();
            return computer.Output.Last();
        }
    }
}