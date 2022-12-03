using ElfRPS;
using Xunit;

namespace ElfRPSTests
{
    public class StarOneTests
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
            var expected = 15;

            var actual = RPSGame.PlayUsingInstructionsForHands(_input);

            Assert.Equal(expected, actual);
        }
    }
}
