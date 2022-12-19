using System;
using System.Linq;
using System.Threading.Tasks;

namespace BoilingBoulders
{
    public class Program
    {
        static async Task Main(string[] _)
        {
            var part2 = false;

            var input = await System.IO.File.ReadAllLinesAsync("./inputData/input.txt");
            var cubes = SurfaceAreaCalculator.ParseInput(input).ToHashSet();
            var surfaceArea = SurfaceAreaCalculator.CalculateSurfaceArea(cubes, part2);

            Console.WriteLine($"Total surface area for part {(part2 ? 2 : 1)}: {surfaceArea}");
        }
    }
}
