using System;

namespace AdventOfCode
{
    public class IntcodeComputer
    {
        private int[] _program;
        private int[] _memory;
        private int _ip = 0;
        private int _clock;


        public bool Halted { get; private set; } = false;
        public int InstructionPointer => _ip;
        public int GetMemoryValueAt(int location) => _memory[location];
        public void SetMemoryValueAt(int location, int value) => _memory[location] = value;

        public IntcodeComputer(int[] program)
        {
            _program = program;
            Reset();
        }

        public void RunUntilHalt()
        {
            while (!Halted) Step();
        }

        public void Step()
        {
            switch (_memory[_ip])
            {
                case 1: // ADD
                    _memory[_memory[_ip + 3]] = _memory[_memory[_ip + 1]] + _memory[_memory[_ip + 2]];
                    _ip += 4;
                    break;
                case 2: // MULTIPLY
                    _memory[_memory[_ip + 3]] = _memory[_memory[_ip + 1]] * _memory[_memory[_ip + 2]];
                    _ip += 4;
                    break;
                case 99: // HALT / INVALID
                    Halted = true;
                    break;
                default:
                    Console.WriteLine($"Invalid instruction {_memory[_ip]}! IP: {_ip}, CLK: {_clock}");
                    Halted = true;
                    break;
            }

            _clock++;
        }

        public void Reset()
        {
            _memory = new int[_program.Length];
            _program.CopyTo(_memory, 0);
            Halted = false;
            _ip = 0;
            _clock = 0;
        }
    }
}