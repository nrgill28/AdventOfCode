using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 7, Name = "Amplification Circuit")]
    public sealed class AoC2019Day7 : AoCSolution
    {
        public override object RunPart1()
        {
            // Get the program and make 5 computers and chain their inputs / outputs
            var program = InputAsIntcode;
            var computers = new IntcodeComputer[5];
            for (var i = 0; i < computers.Length; i++)
            {
                computers[i] = new IntcodeComputer(program, $"Computer {i}");
                if (i != 0) computers[i].SetInput(computers[i - 1]);
            }

            // With all the permutations of 0, 1, 2, 3, 4
            var max = long.MinValue;
            foreach (var perm in new List<int> {0, 1, 2, 3, 4}.GetPermutations())
            {
                // Reset the computers and queue their phase setting
                for (int i = 0; i < computers.Length; i++)
                {
                    computers[i].Reset();
                    computers[i].QueueInput(perm[i]);
                }
                
                // Queue the first input
                computers[0].QueueInput(0);
                
                // Get the first output from the last computer and check if it is greater than the last max
                var output = computers[^1].NextOutput();
                if (output > max) max = output.Value;
            }

            return max;
        }

        public override object RunPart2()
        {
            // Get the program and make 5 computers and chain their inputs / outputs
            var program = InputAsIntcode;
            var computers = new IntcodeComputer[5];
            for (var i = 0; i < computers.Length; i++)
            {
                computers[i] = new IntcodeComputer(program, $"Computer {i}");
                if (i != 0) computers[i].SetInput(computers[i - 1]);
                if (i == computers.Length - 1) computers[0].SetInput(computers[i]);
            }
            
            // With all the permutations of 5, 6, 7, 8, 9
            var max = long.MinValue;
            foreach (var perm in new List<int> {5, 6, 7, 8, 9}.GetPermutations())
            {
                // Reset the computers and give them their input setting
                for (int i = 0; i < computers.Length; i++)
                {
                    computers[i].Reset();
                    computers[i].QueueInput(perm[i]);
                }
                
                // Enqueue the first input
                computers[0].QueueInput(0);
                
                // Let the last computer run until it halts
                computers[^1].RunUntilHalt();
                
                // Get the last value and check if it's greater
                var output = computers[^1].Output.Last();
                if (output > max) max = output;
            }

            return max;
        }
        
    }
}