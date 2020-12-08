using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    [AoCSolution(Year = 2020, Day = 8, Name = "Handheld Halting")]
    public sealed class AoC2020Day8 : AoCSolution
    {
        private Tuple<string, int>[] _program;
        
        public override void Setup()
        {
            var lines = InputAsLines;
            _program = new Tuple<string, int>[lines.Length];

            // For each line in the input, parse it to an array of (string, int) tuples
            for (var i = 0; i < lines.Length; i++)
            {
                var split = lines[i].Split(" ");
                _program[i] = new Tuple<string, int>(split[0], int.Parse(split[1]));
            }
        }

        public override object RunPart1()
        {
            // Run the program as-is and return the accumulator value
            Terminates(_program, out var acc);
            return acc;
        }

        public override object RunPart2()
        {
            // For each instruction in the program
            for (var i = 0; i < _program.Length; i++)
            {
                // Unpack the instruction
                var (instruction, param) = _program[i];
                
                switch (instruction)
                {
                    // If this instruction is 'acc', skip.
                    case "acc":
                        continue;
                    // If this instruction is a nop, replace it with a jmp
                    case "nop":
                        _program[i] = new Tuple<string, int>("jmp", param);
                        break;
                    // If this instruction is a jmp, replace it with a nop
                    default:
                        _program[i] = new Tuple<string, int>("nop", param);
                        break;
                }

                // Check if the program now terminates
                if (Terminates(_program, out var acc))
                    // If it does, return the accumulator value
                    return acc;

                // If it doesn't, reset the instruction and continue the loop
                _program[i] = new Tuple<string, int>(instruction, param);
            }

            // Shouldn't happen if the input is valid
            return default;
        }

        private static bool Terminates(IReadOnlyList<Tuple<string, int>> program, out int accumulator)
        {
            // Declare some variables
            var ip = 0;
            accumulator = 0;
            var executed = new bool[program.Count];

            // While the program is running
            while (ip < program.Count)
            {
                // If we've already executed this instruction it means there is an infinite loop
                if (executed[ip]) return false;
                
                // Otherwise we can go ahead and execute it
                var (instruction, param) = program[ip];
                executed[ip] = true;
                
                switch (instruction)
                {
                    // nop does nothing. Just increment ip
                    case "nop":
                        ip++;
                        break;
                    // acc adds the parameter value to the accumulator
                    case "acc":
                        accumulator += param;
                        ip++;
                        break;
                    // jmp adds the parameter value to the instruction pointer
                    case "jmp":
                        ip += param;
                        break;
                }
            }

            // If the loop finished, the program terminated.
            return true;
        }
    }
}