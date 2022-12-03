using CalorieCounting;

namespace CalorieCountingTests
{
    public class StarOneTests
    {
        private static readonly string[] _testInput = @"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000".Split(Environment.NewLine);

        [Fact]
        public void FirstExamplePasses()
        {
            var expected = 24000;

            var actual = CalorieCounter.FindMostCaloriesCarriedBySingleElf(_testInput);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SecondExamplePasses()
        {
            var expected = 45000;

            var actual = CalorieCounter.FindMostCaloriesCarriedByTopThreeElves(_testInput);

            Assert.Equal(expected, actual);
        }
    }
}
