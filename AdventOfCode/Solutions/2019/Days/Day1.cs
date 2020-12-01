using System.Linq;

namespace AdventOfCode
{
    [AoCSolution(Year = 2019, Day = 1, Name = "The Tyranny of the Rocket Equation")]
    public class AoC2019Day1 : AoCSolution
    {
        public override object RunPart1()
        {
            var input = InputAsInts();
            return input.Sum(FuelRequired);
        }

        public override object RunPart2()
        {
            var input = InputAsInts();

            var sum = 0;
            foreach (var module in input)
            {
                var fuelRequired = FuelRequired(module);
                sum += fuelRequired;
                while ((fuelRequired = FuelRequired(fuelRequired)) > 0)
                {
                    sum += fuelRequired;
                }
            }

            return sum;
        }

        private int FuelRequired(int mass) => mass / 3 - 2;
    }
}