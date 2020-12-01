using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020
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
            var attrib = Attribute;
            var path = string.Format(InputPath, attrib.Year, attrib.Day);
            Input = File.ReadAllText(path);
        }
        
        /// <summary>
        ///     The day's input text, set by the Main method.
        /// </summary>
        protected string Input { get; }
        
        protected int[] InputAsInts(char sep = '\n') => Input.Split(sep)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(int.Parse)
            .ToArray();
        

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

        public static IEnumerable<AoCSolution> GetDays()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsSubclassOf(typeof(AoCSolution)))
                .Select(Activator.CreateInstance)
                .Cast<AoCSolution>();
        }
        
        public AoCSolutionAttribute Attribute => GetType().GetCustomAttributes(typeof(AoCSolutionAttribute), false)[0] as AoCSolutionAttribute;
    }

    public class AoCSolutionAttribute : Attribute
    {
        public int Day { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
    }
}