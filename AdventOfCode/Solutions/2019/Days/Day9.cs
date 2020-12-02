using System;
using System.Linq;
using System.Xml;

namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 9, Name = "Sensor Boost")]
    public class AoC2019Day9 : AoCSolution
    {
        private long[] _program;
        
        public override object RunPart1()
        {
            _program = InputAsIntcode;

            var computer = new IntcodeComputer(_program);
            computer.QueueInput(1);
            return computer.NextOutput();
        }

        public override object RunPart2()
        {
            var computer = new IntcodeComputer(_program);
            computer.QueueInput(2);
            return computer.NextOutput();
        }
    }
}