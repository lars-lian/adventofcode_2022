using System;
using System.IO;
using System.Threading.Tasks;

namespace CalorieCounting
{
    internal class Program
    {
        private static async Task Main(string[] _)
        {
            var inputLines = await File.ReadAllLinesAsync("./inputdata/input");
            var topCalories = CalorieCounter.FindMostCaloriesCarriedBySingleElf(inputLines);
            Console.WriteLine($"Highest amount of calories carried by single elf: {topCalories}");

            var topThreeCaloriesSum = CalorieCounter.FindMostCaloriesCarriedByTopThreeElves(inputLines);
            Console.WriteLine($"Amount of calories carried by top three elves: {topThreeCaloriesSum}");
        }
    }
}
