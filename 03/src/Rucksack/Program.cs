using System;
using System.IO;
using System.Threading.Tasks;
using Rucksack;

internal class Program
{
    private static async Task Main(string[] _)
    {
        var input = await File.ReadAllLinesAsync("./inputdata/input");

        var sumOfPriorities = Elf.FindSumOfPriorities(input);
        Console.WriteLine($"Sum of priorities for {input.Length} rucksacks: {sumOfPriorities}"); // 7597

        var sumOfBadges = Elf.FindSumOfGroupBadgePriorities(input);
        Console.WriteLine($"Sum of badge priorities for {input.Length} rucksacks: {sumOfBadges}"); // 2607
    }
}
