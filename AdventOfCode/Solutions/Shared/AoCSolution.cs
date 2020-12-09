using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode
{
    /// <summary>
    ///     Represents a day in the Advent of Code challenge
    /// </summary>
    public abstract class AoCSolution
    {
        /// <summary>
        ///     Format string that points to a day's input
        /// </summary>
        private const string InputPath = "Inputs/{0}/input_day{1}.txt";
        protected AoCSolution()
        {

        }
        
        /// <summary>
        ///     The day's input text, set by the Main method.
        /// </summary>
        protected string Input { get; set; }

        protected virtual string DebugInput => null;

        protected int[] InputAsInts(char sep = '\n') => Input.Split(sep)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse)
            .ToArray();

        protected long[] InputAsLongs(char sep = '\n') => Input.Split(sep)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(long.Parse)
            .ToArray();
        
        protected long[] InputAsIntcode => Input.Split(',')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(long.Parse)
            .ToArray();

        protected string[] InputAsLines => Input.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();


        /// <summary>
        ///     Executes the day's part 1 and returns the result
        /// </summary>
        public abstract object RunPart1();

        /// <summary>
        ///     Executes the day's part 2 and returns the result
        /// </summary>
        public abstract object RunPart2();

        public virtual void Setup()
        {
            
        }

        public void Init()
        {
            var attrib = Attribute;
            var path = string.Format(InputPath, attrib.Year, attrib.Day);
            Input = DebugInput ?? File.ReadAllText(path);
            Setup();
        }

        public static IEnumerable<AoCSolution> GetDays()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsSubclassOf(typeof(AoCSolution)))
                .Select(Activator.CreateInstance)
                .Cast<AoCSolution>();
        }
        
        public AoCSolutionAttribute Attribute => GetType().GetCustomAttributes(typeof(AoCSolutionAttribute), false)[0] as AoCSolutionAttribute;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AoCSolutionAttribute : Attribute
    {
        public int Day { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
    }
}