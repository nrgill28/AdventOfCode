using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class IntcodeComputer
    {
        private int[] _program;
        private int[] _memory;
        private int _ip = 0;
        private int _clock;

        private IntcodeComputer _inputRef;

        private Queue<int> _input = new();
        public Queue<int> Output = new();

        public string Name;
        
        public bool Halted { get; private set; } = false;
        public bool WaitingForInput { get; private set; } = false;
        public int InstructionPointer => _ip;

        public int GetMemoryValue(int parameter, int mode = 0)
        {
            return mode switch
            {
                0 => _memory[parameter],
                1 => parameter,
                _ => default
            };
        }
        public void SetMemoryValue(int parameter, int value, int mode = 0)
        {
            switch (mode)
            {
                case 0:
                    _memory[parameter] = value;
                    break;
            }
        }

        public IntcodeComputer(int[] program, string name = null)
        {
            _program = program;
            Reset();
            Name = name;
        }

        public void RunUntilHalt()
        {
            while (!Halted && !WaitingForInput) Step();
        }

        public void Step()
        {
            int opcode = _memory[_ip] % 100,
                mode1 = _memory[_ip] % 1000 / 100,
                mode2 = _memory[_ip] % 10000 / 1000,
                mode3 = _memory[_ip] / 10000;
            switch (opcode)
            {
                case 1: // ADD
                    var aOp1 = GetMemoryValue(_memory[_ip + 1], mode1);
                    var aOp2 = GetMemoryValue(_memory[_ip + 2], mode2);
                    SetMemoryValue(_memory[_ip + 3], aOp1 + aOp2, mode3);
                    _ip += 4;
                    break;
                case 2: // MULTIPLY
                    var mOp1 = GetMemoryValue(_memory[_ip + 1], mode1);
                    var mOp2 = GetMemoryValue(_memory[_ip + 2], mode2);
                    SetMemoryValue(_memory[_ip + 3], mOp1 * mOp2, mode3);
                    _ip += 4;
                    break;
                case 3: // INPUT
                    var inp = NextInput();
                    if (!inp.HasValue) WaitingForInput = true;
                    else
                    {
                        WaitingForInput = false;
                        SetMemoryValue(_memory[_ip + 1], inp.Value, mode1);
                        _ip += 2;
                    }
                    break;
                case 4: // OUTPUT
                    WriteOutput(GetMemoryValue(_memory[_ip + 1], mode1));
                    _ip += 2;
                    break;
                case 5: // JUMP IF TRUE
                    var eqOp1 = GetMemoryValue(_memory[_ip + 1], mode1);
                    var eqOp2 = GetMemoryValue(_memory[_ip + 2], mode2);
                    if (eqOp1 != 0) _ip = eqOp2;
                    else _ip += 3;
                    break;
                case 6: // JUMP IF FALSE
                    var neOp1 = GetMemoryValue(_memory[_ip + 1], mode1);
                    var neOp2 = GetMemoryValue(_memory[_ip + 2], mode2);
                    if (neOp1 == 0) _ip = neOp2;
                    else _ip += 3;
                    break;
                case 7: // LESS THAN
                    var ltOp1 = GetMemoryValue(_memory[_ip + 1], mode1);
                    var ltOp2 = GetMemoryValue(_memory[_ip + 2], mode2);
                    SetMemoryValue(_memory[_ip + 3], ltOp1 < ltOp2 ? 1 : 0, mode3);
                    _ip += 4;
                    break;
                case 8: // Equals
                    var gtOp1 = GetMemoryValue(_memory[_ip + 1], mode1);
                    var gtOp2 = GetMemoryValue(_memory[_ip + 2], mode2);
                    SetMemoryValue(_memory[_ip + 3], gtOp1 == gtOp2 ? 1 : 0, mode3);
                    _ip += 4;
                    break;
                case 99: // HALT
                    Halted = true;
                    break;
                default: // INVALID
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
            _input.Clear();
        }

        private int? NextInput()
        {
            // If we already have some input queued, return it
            if (_input.Count > 0)
                return _input.Dequeue();
            
            // If we have a connected computer, try getting the input from there
            var output = _inputRef?.NextOutput();
            
            // Then return
            return output;
        }

        private void WriteOutput(int x)
        {
            Output.Enqueue(x);
        }

        public void QueueInput(int x)
        {
            _input.Enqueue(x);
        }
        
        public void SetInput(IntcodeComputer other)
        {
            _input = other.Output;
            _inputRef = other;
        }

        public int? NextOutput()
        {
            while (Output.Count == 0 && !Halted && !WaitingForInput) Step();
            if (WaitingForInput)
            {
                Console.WriteLine($"[Err]: Computer is waiting for input. Possible deadlock. ({Name})");
                return null;
            }
            if (Output.Count == 0) return null;
            return Output.Dequeue();
        }
    }
}