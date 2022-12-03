using ElfRPS;
using Xunit;

namespace ElfRPSTests
{
    public class StarTwoTests
    {
        private static readonly string[] _input = new string[]
        {
            "A Y",
            "B X",
            "C Z"
        };

        [Fact]
        public void PlayByRules_MatchesExample()
        {
            var expected = 12;

            var actual = RPSGame.PlayUsingInstructionsForDesiredOutcome(_input);

            Assert.Equal(expected, actual);
        }
    }
}
