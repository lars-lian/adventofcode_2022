using System.Linq;

namespace CalorieCounting
{
    public static class CalorieCounter
    {
        public static int FindMostCaloriesCarriedBySingleElf(string[] inputLines) =>
            AggregateCaloriePerElf(inputLines).Max();

        public static int FindMostCaloriesCarriedByTopThreeElves(string[] inputLines) =>
            AggregateCaloriePerElf(inputLines)
                .OrderByDescending(x => x)
                .Take(3)
                .Sum();

        private static int[] AggregateCaloriePerElf(string[] inputLines)
        {
            var elfN = 0;
            var elfCount = inputLines.Where(string.IsNullOrWhiteSpace).Count() + 1;
            var elfCalories = new int[elfCount];

            foreach (var line in inputLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    elfCalories[elfN] += int.Parse(line);
                }
                else
                {
                    elfN++;
                }
            }

            return elfCalories;
        }
    }
}
