using System;
using Rucksack;

namespace RucksackTests
{
    public class StarOneTests
    {
        private static readonly string[] _input = @"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw".Split(Environment.NewLine);

        [Theory]
        [InlineData('a', 1)]
        [InlineData('b', 2)]
        [InlineData('c', 3)]
        public void Finds_Priority_Lowercase(char arg, int expected)
        {
            var actual = Elf.GetPriority(arg);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('A', 27)]
        [InlineData('B', 28)]
        [InlineData('C', 29)]
        public void Finds_Priority_Uppercase(char arg, int expected)
        {
            var actual = Elf.GetPriority(arg);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Gets_Compartments()
        {
            var first = _input[0];

            var compartments = Elf.GetCompartments(first);

            var actual = string.Concat(compartments.Item1, compartments.Item2);

            Assert.Equal(first, actual);
        }

        [Theory]
        [InlineData(0, 'p')]
        [InlineData(1, 'L')]
        [InlineData(2, 'P')]
        [InlineData(3, 'v')]
        [InlineData(4, 't')]
        [InlineData(5, 's')]
        public void Finds_SharedItem(int inputLine, char expected)
        {
            var line = _input[inputLine];

            var actual = Elf.GetSharedItem(line);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Example_Passes()
        {
            var expected = 157;

            var actual = Elf.FindSumOfPriorities(_input);

            Assert.Equal(expected, actual);
        }
    }
}
