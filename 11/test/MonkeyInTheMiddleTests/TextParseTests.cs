using System.Linq;
using MonkeyInTheMiddle;

namespace MonkeyInTheMiddleTests
{
    public class TextParseTests
    {
        private static readonly string[] _exampleInput = new string[] {
            "Monkey 0:",
            "  Starting items: 79, 98",
            "  Operation: new = old * 19",
            "  Test: divisible by 23",
            "    If true: throw to monkey 2",
            "    If false: throw to monkey 3",
            "",
            "Monkey 1:",
            "  Starting items: 54, 65, 75, 74",
            "  Operation: new = old + 6",
            "  Test: divisible by 19",
            "    If true: throw to monkey 2",
            "    If false: throw to monkey 0",
            "",
            "Monkey 2:",
            "  Starting items: 79, 60, 97",
            "  Operation: new = old * old",
            "  Test: divisible by 13",
            "    If true: throw to monkey 1",
            "    If false: throw to monkey 3",
            "",
            "Monkey 3:",
            "  Starting items: 74",
            "  Operation: new = old + 3",
            "  Test: divisible by 17",
            "    If true: throw to monkey 0",
            "    If false: throw to monkey 1",
        };

        [Fact]
        public void CanCreate_Monkey()
        {
            var monkey = MonkeyBusinessParser.CreateMonkey(_exampleInput[..7]);

            Assert.True(monkey.HeldItems.Any());
        }

        [Fact]
        public void CanCreate_Monkeys()
        {
            var expectedCount = 4;
            var monkeys = MonkeyBusinessParser.CreateMonkeys(_exampleInput);

            Assert.Equal(expectedCount, monkeys.Length);
            Assert.All(monkeys, x => Assert.True(x.HeldItems.Any()));
        }

        [Fact]
        public void ParseStartingItems_ParsesStartingItems()
        {
            var input = "  Starting items: 1, 23, 45";
            var expected = new ulong[] { 1, 23, 45 };

            var actual = MonkeyBusinessParser.ParseStartingItems(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MatchStartingItems_MatchesStartingItems()
        {
            var input = "  Starting items: 1, 23, 45";
            var expected = new string[] { "1", "23", "45" };

            var match = MonkeyBusinessParser.MatchStartingItems(input);
            var items = match.Groups["Items"];
            var captures = items.Captures;
            var actual = new string[]
            {
                captures[0].Value,
                captures[1].Value,
                captures[2].Value
            };

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("  Operation: new = old * 19", "*", "old", "19")]
        [InlineData("  Operation: new = old + 6", "+", "old", "6")]
        [InlineData("  Operation: new = old * old", "*", "old", "old")]
        [InlineData("  Operation: new = old + 3", "+", "old", "3")]
        public void MatchOperation_MatchesNecessaryGroups(string arg, string expectedOperator, string expectedFactor1, string expectedFactor2)
        {
            var match = MonkeyBusinessParser.MatchOperation(arg);
            var actualOperator = match.Groups["Operator"].Value;
            var actualFactor1 = match.Groups["Factor1"].Value;
            var actualFactor2 = match.Groups["Factor2"].Value;

            Assert.Equal(expectedOperator, actualOperator);
            Assert.Equal(expectedFactor1, actualFactor1);
            Assert.Equal(expectedFactor2, actualFactor2);
        }

        [Theory]
        [InlineData("  Test: divisible by 23", "divisible", "23")]
        [InlineData("  Test: divisible by 19", "divisible", "19")]
        [InlineData("  Test: divisible by 13", "divisible", "13")]
        [InlineData("  Test: divisible by 17", "divisible", "17")]
        public void MatchTestCondition_MatchesFactor(string arg, string expectedOperation, string expectedFactor)
        {
            var match = MonkeyBusinessParser.MatchTestCondition(arg);
            var actualOperation = match.Groups["Operation"].Value;
            var actualFactor = match.Groups["Factor"].Value;

            Assert.Equal(expectedOperation, actualOperation);
            Assert.Equal(expectedFactor, actualFactor);
        }

        [Theory]
        [InlineData("    If true: throw to monkey 0", "0")]
        [InlineData("    If true: throw to monkey 1", "1")]
        [InlineData("    If true: throw to monkey 2", "2")]
        public void MatchTrueResults_MatchesTargetMonkey(string arg, string expected)
        {
            var match = MonkeyBusinessParser.MatchTrueResult(arg);
            var actual = match.Groups["Monkey"].Value;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("    If false: throw to monkey 0", "0")]
        [InlineData("    If false: throw to monkey 1", "1")]
        [InlineData("    If false: throw to monkey 2", "2")]
        public void MatchFalseResults_MatchesTargetMonkey(string arg, string expected)
        {
            var match = MonkeyBusinessParser.MatchFalseResult(arg);
            var actual = match.Groups["Monkey"].Value;

            Assert.Equal(expected, actual);
        }
    }
}
