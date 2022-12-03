using System;
using System.IO;
using System.Threading.Tasks;

namespace ElfRPS
{
    internal static class Program
    {
        private static async Task Main(string[] _)
        {
            var inputLines = await File.ReadAllLinesAsync("./inputdata/input");

            var starOneScore = RPSGame.PlayUsingInstructionsForHands(inputLines);
            Console.WriteLine($"Star one final score: {starOneScore}");

            var starTwoScore = RPSGame.PlayUsingInstructionsForDesiredOutcome(inputLines);
            Console.WriteLine($"Star two final score: {starTwoScore}");
        }
    }
}
