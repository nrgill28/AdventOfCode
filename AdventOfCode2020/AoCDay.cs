using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    /// <summary>
    ///     Represents a day in the Advent of Code challenge
    /// </summary>
    public abstract class AoCDay
    {
        /// <summary>
        ///     The day's input text, set by the Main method.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        ///     Helper property to get the input as a sequence of integers separated by newlines
        /// </summary>
        protected IEnumerable<int> InputAsInts =>
            Input.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse);

        /// <summary>
        ///     Executes the day's part 1 and returns the result
        /// </summary>
        public virtual int RunPart1()
        {
            return default;
        }

        /// <summary>
        ///     Executes the day's part 2 and returns the result
        /// </summary>
        public virtual int RunPart2()
        {
            return default;
        }
    }
}