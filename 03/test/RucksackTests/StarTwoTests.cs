using System;
using System.Linq;
using Rucksack;

namespace RucksackTests
{
    public class StarTwoTests
    {
        private static readonly string[] _input =
@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw".Split(Environment.NewLine);

        [Theory]
        [InlineData(0, 'r')]
        [InlineData(1, 'Z')]
        public void Finds_GroupBadge(int groupNumber, char expected)
        {
            var groupedGroupRucksacks = Elf.GetGroupRucksacks(_input);
            var groupRucksacks = groupedGroupRucksacks.ElementAt(groupNumber);

            var actual = Elf.GetGroupBadge(groupRucksacks);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Finds_GroupPrioritySum()
        {
            var expected = 70;

            var actual = Elf.FindSumOfGroupBadgePriorities(_input);

            Assert.Equal(expected, actual);
        }
    }
}
