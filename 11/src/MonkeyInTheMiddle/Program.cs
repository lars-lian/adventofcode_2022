using System;
using System.IO;
using System.Threading.Tasks;

namespace MonkeyInTheMiddle
{
    internal class Program
    {
        private static async Task Main(string[] _)
        {
            var inputLines = await File.ReadAllLinesAsync("./inputdata/input");
            var starOneResult = Runner.Run(inputLines, 20, decreaseWorry: true);
            var starTwoResult = Runner.Run(inputLines, 10_000, decreaseWorry: false);

            Console.WriteLine($"Star one: {starOneResult}");
            Console.WriteLine($"Star two: {starTwoResult}");
        }
    }
}
