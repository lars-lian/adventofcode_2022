using System;
using System.Linq;

namespace ElfRPS
{
    public static class RPSGame
    {
        public static int GetResultForGivenHand(Hand theirs, Hand ours) => (int)ours.PlayAgainst(theirs) + (int)ours.RPSValue;

        public static int GetResultForGivenOutcome(Hand theirs, Hand ours)
        {
            switch (ours.TranslateAsDesiredOutcome())
            {
                case RPSResult.Lose:
                    var losingHand = theirs.WinsOver();
                    return (int)losingHand.PlayAgainst(theirs) + (int)losingHand.RPSValue;
                case RPSResult.Draw:
                    var equalHand = theirs;
                    return (int)theirs.PlayAgainst(equalHand) + (int)equalHand.RPSValue;
                case RPSResult.Win:
                    var winningHand = theirs.LosesTo();
                    return (int)winningHand.PlayAgainst(theirs) + (int)winningHand.RPSValue;
                default:
                    throw new ArgumentOutOfRangeException(ours.Symbol.ToString());
            }
        }

        public static int PlayUsingInstructionsForHands(string[] input) => Play(input, GetResultForGivenHand);

        public static int PlayUsingInstructionsForDesiredOutcome(string[] input) => Play(input, GetResultForGivenOutcome);

        private static int Play(string[] input, Func<Hand, Hand, int> strategy) =>
            input.Aggregate
            (
                0,
                (acc, line) => acc + strategy.Invoke(new Hand(line.Split(' ')[0][0]), new Hand(line.Split(' ')[1][0]))
            );
    }
}
