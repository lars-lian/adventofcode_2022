using System;

namespace ElfRPS
{
    public class Hand
    {
        public RPSValue RPSValue { get; }
        public char Symbol { get; }

        public Hand(char symbol)
        {
            Symbol = symbol;

            RPSValue = symbol switch
            {
                'A' or 'X' => RPSValue.Rock,
                'B' or 'Y' => RPSValue.Paper,
                'C' or 'Z' => RPSValue.Scissors,
                _ => throw new ArgumentOutOfRangeException(symbol.ToString()),
            };
        }

        public RPSResult PlayAgainst(Hand theirs) => theirs.RPSValue == RPSValue
                ? RPSResult.Draw
                : theirs.RPSValue switch
                {
                    RPSValue.Rock => RPSValue == RPSValue.Paper ? RPSResult.Win : RPSResult.Lose,
                    RPSValue.Paper => RPSValue == RPSValue.Scissors ? RPSResult.Win : RPSResult.Lose,
                    RPSValue.Scissors => RPSValue == RPSValue.Rock ? RPSResult.Win : RPSResult.Lose,
                    _ => throw new ArgumentOutOfRangeException(theirs.RPSValue.ToString()),
                };

        public Hand LosesTo() => RPSValue switch
        {
            RPSValue.Rock => new Hand('Y'),
            RPSValue.Paper => new Hand('Z'),
            RPSValue.Scissors => new Hand('X'),
            _ => throw new ArgumentOutOfRangeException(),
        };

        public Hand WinsOver() => RPSValue switch
        {
            RPSValue.Rock => new Hand('Z'),
            RPSValue.Paper => new Hand('X'),
            RPSValue.Scissors => new Hand('Y'),
            _ => throw new ArgumentOutOfRangeException(),
        };

        public RPSResult TranslateAsDesiredOutcome() => Symbol switch
        {
            'X' => RPSResult.Lose,
            'Y' => RPSResult.Draw,
            'Z' => RPSResult.Win,
            _ => throw new ArgumentOutOfRangeException(Symbol.ToString()),
        };
    }
}
