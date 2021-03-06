﻿using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    ///     Class to represent an Intcode Computer
    /// </summary>
    public class IntcodeComputer
    {
        private readonly long[] _program;

        public readonly Queue<long> Output = new();
        private bool _halted, _waitingForInput;
        private Queue<long> _input = new();
        private IntcodeComputer _inputRef;
        private long _ip, _rp, _clock;
        private long[] _memory;

        public IntcodeComputer(long[] program, string name = null)
        {
            _program = program;
            Reset();
        }

        /// <summary>
        ///     Given the parameter and mode, get that value in memory
        /// </summary>
        public long GetMemoryValue(long parameter, long mode = 0)
        {
            return mode switch
            {
                0 => _memory[parameter],
                1 => parameter,
                2 => _memory[_rp + parameter],
                _ => default
            };
        }

        /// <summary>
        ///     Given the parameter and mode, set that value in memory.
        /// </summary>
        public void SetMemoryValue(long parameter, long value, long mode = 0)
        {
            switch (mode)
            {
                case 0:
                    if (parameter >= _memory.Length) GrowMemory(parameter + 1);
                    _memory[parameter] = value;
                    break;
                case 1:
                    throw new InvalidOperationException("You cannot set memory values in immediate mode!");
                case 2:
                    if (_rp + parameter >= _memory.Length) GrowMemory(_rp + parameter + 1);
                    _memory[_rp + parameter] = value;
                    break;
            }
        }

        /// <summary>
        ///     Continuously executes instructions until the computer halts or breaks due to missing input
        /// </summary>
        public void RunUntilHalt()
        {
            while (!_halted && !_waitingForInput) Step();
        }

        /// <summary>
        ///     Let the computer perform a step. This does not always mean it will execute an
        ///     instruction, if the current operation is to retrieve input and there is none available
        ///     the computer will pause execution.
        /// </summary>
        private void Step()
        {
            var opcode = _memory[_ip] % 100;
            long Param(int i) => GetMemoryValue(_memory[_ip + 1 + i], _memory[_ip].DigitAt(2 + i));
            void Result(int i, long v) => SetMemoryValue(_memory[_ip + 1 + i], v, _memory[_ip].DigitAt(2 + i));

            switch (opcode)
            {
                case 1: // ADD
                    Result(2, Param(0) + Param(1));
                    _ip += 4;
                    break;
                case 2: // MULTIPLY
                    Result(2, Param(0) * Param(1));
                    _ip += 4;
                    break;
                case 3: // INPUT
                    var inp = NextInput();
                    _waitingForInput = !inp.HasValue;
                    if (!inp.HasValue) return;
                    else
                    {
                        Result(0, inp.Value);
                        _ip += 2;
                        break;
                    }
                case 4: // OUTPUT
                    Output.Enqueue(Param(0));
                    _ip += 2;
                    break;
                case 5: // JUMP IF TRUE
                    if (Param(0) != 0) _ip = Param(1);
                    else _ip += 3;
                    break;
                case 6: // JUMP IF FALSE
                    if (Param(0) == 0) _ip = Param(1);
                    else _ip += 3;
                    break;
                case 7: // LESS THAN
                    Result(2, Param(0) < Param(1) ? 1 : 0);
                    _ip += 4;
                    break;
                case 8: // Equals
                    Result(2, Param(0) == Param(1) ? 1 : 0);
                    _ip += 4;
                    break;
                case 9:
                    _rp += Param(0);
                    _ip += 2;
                    break;
                case 99: // HALT
                    _halted = true;
                    break;
                default: // INVALID
                    Console.WriteLine($"Invalid instruction {_memory[_ip]}! IP: {_ip}, CLK: {_clock}");
                    _halted = true;
                    break;
            }

            _clock++;
        }

        /// <summary>
        ///     Resets the computer to it's initial state
        /// </summary>
        public void Reset()
        {
            _memory = new long[_program.Length];
            _program.CopyTo(_memory, 0);
            _halted = false;
            _ip = 0;
            _rp = 0;
            _clock = 0;
            _input.Clear();
        }

        /// <summary>
        ///     Tried to get the next input in the queue, or if we are using the output of another computer
        ///     as input, running that program until it produces an output.
        /// </summary>
        /// <returns>The next input if available, otherwise null</returns>
        private long? NextInput()
        {
            // If we already have some input queued, return it
            if (_input.Count > 0)
                return _input.Dequeue();

            // If we have a connected computer, try getting the input from there
            var output = _inputRef?.NextOutput();

            // Then return
            return output;
        }

        /// <summary>
        ///     Adds a value to the input queue
        /// </summary>
        public void QueueInput(long x) => _input.Enqueue(x);

        /// <summary>
        ///     This method is used to set the input of this computer to the output of another
        /// </summary>
        /// <param name="other">The other computer to use as input</param>
        public void SetInput(IntcodeComputer other)
        {
            _input = other.Output;
            _inputRef = other;
        }

        /// <summary>
        ///     Runs the computer until it has something in the output, then returns that value.
        /// </summary>
        /// <returns>The next available value in the output</returns>
        public long? NextOutput()
        {
            while (Output.Count == 0 && !_halted && !_waitingForInput) Step();
            if (_waitingForInput)
            {
                Console.WriteLine("[Err]: Computer is waiting for input. Possible deadlock.");
                return null;
            }

            if (Output.Count == 0) return null;
            return Output.Dequeue();
        }

        private void GrowMemory(long size)
        {
            var foo = new long[size];
            _memory.CopyTo(foo, 0);
            _memory = foo;
        }
    }
}