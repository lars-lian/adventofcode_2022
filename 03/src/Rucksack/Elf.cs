using System;
using System.Collections.Generic;
using System.Linq;

namespace Rucksack
{
    public static class Elf
    {
        public const int GroupSize = 3;

        public static int FindSumOfPriorities(string[] ruckSacks) => ruckSacks.Aggregate(0, (acc, rs) => acc + GetSharedItemPriority(rs));

        public static int FindSumOfGroupBadgePriorities(string[] rucksacks) => GetGroupRucksacks(rucksacks)
                .Aggregate(0, (acc, groupRucksacks) => acc += GetPriority(GetGroupBadge(groupRucksacks)));

        public static int GetPriority(char symbol) => symbol < 'a'
            ? symbol - 'A' + 27
            : symbol - 'a' + 1;

        public static Tuple<string, string> GetCompartments(string arg)
        {
            var first = arg[..(arg.Length / 2)];
            var second = arg[(arg.Length / 2)..];

            return new Tuple<string, string>(first, second);
        }

        public static IEnumerable<string[]> GetGroupRucksacks(string[] arg)
        {
            for (var i = 0; i < arg.Length; i += GroupSize)
            {
                var groupRucksacks = new string[GroupSize];
                Array.Copy(arg, i, groupRucksacks, 0, GroupSize);
                yield return groupRucksacks;
            }
        }

        public static char GetSharedItem(string rucksack)
        {
            var compartments = GetCompartments(rucksack);
            return compartments.Item1.Intersect(compartments.Item2).First();
        }

        public static int GetSharedItemPriority(string rucksack)
        {
            var sharedItem = GetSharedItem(rucksack);
            return GetPriority(sharedItem);
        }

        public static char GetGroupBadge(string[] groupRucksacks)
        {
            foreach (var sharedItem in groupRucksacks[0].Intersect(groupRucksacks[1]))
            {
                if (groupRucksacks[2].IndexOf(sharedItem) != -1)
                {
                    return sharedItem;
                }
            }

            throw new InvalidOperationException();
        }
    }
}
