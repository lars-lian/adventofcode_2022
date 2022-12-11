using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace MonkeyInTheMiddle
{
    public static class MonkeyBusinessParser
    {
        private const int LinesPerMonkeyInput = 7;
        private const string RegexPatternStartingItems = @"^  Starting items: (\s?(?<Items>\d+)+,?)+$";
        private const string RegexPatternOperation = @"^  Operation: new = (?<Factor1>.+) (?<Operator>.) (?<Factor2>.+)$";
        private const string RegexPatternTestCondition = @"^  Test: (?<Operation>.+) by (?<Factor>.+)$";
        private const string RegexPatternIfTrueResult = @"^    If true: throw to monkey (?<Monkey>\d+)$";
        private const string RegexPatternIfFalseResult = @"^    If false: throw to monkey (?<Monkey>\d+)$";

        public static Match MatchStartingItems(string arg) => Regex.Match(arg, RegexPatternStartingItems);
        public static Match MatchOperation(string arg) => Regex.Match(arg, RegexPatternOperation);
        public static Match MatchTestCondition(string arg) => Regex.Match(arg, RegexPatternTestCondition);
        public static Match MatchTrueResult(string arg) => Regex.Match(arg, RegexPatternIfTrueResult);
        public static Match MatchFalseResult(string arg) => Regex.Match(arg, RegexPatternIfFalseResult);

        public static Monkey CreateMonkey(string[] monkeyInput)
        {
            var name = monkeyInput[0][6..];
            var startingItems = ParseStartingItems(monkeyInput[1]);
            var matched = MatchOperation(monkeyInput[2]);
            var operate = BuildOperationExpression(matched);
            var tester = BuildTestExpression(monkeyInput[3]);
            var trueTarget = int.Parse(MatchTrueResult(monkeyInput[4]).Groups["Monkey"].Value);
            var falseTarget = int.Parse(MatchFalseResult(monkeyInput[5]).Groups["Monkey"].Value);

            return new Monkey
            {
                Name = name,
                HeldItems = new Queue<ulong>(startingItems),
                TrueTarget = trueTarget,
                FalseTarget = falseTarget,
                MonkeyBusinessLogic = new MonkeyBusinessLogic
                {
                    Operate = operate,
                    Test = tester,
                    TargetMonkeyFalse = falseTarget,
                    TargetMonkeyTrue = trueTarget
                }
            };
        }

        public static Monkey[] CreateMonkeys(string[] input) =>
            InternalCreateMonkeys(input).ToArray();

        private static IEnumerable<Monkey> InternalCreateMonkeys(string[] input)
        {
            for (var i = 0; i < input.Length; i += LinesPerMonkeyInput)
                yield return CreateMonkey(input[i..(i + LinesPerMonkeyInput - 1)]);
        }

        public static BlockExpression BuildExpressionBody(string monkeyBusinessOperator, ParameterExpression oldItemParameter, Expression cexp) => Expression.Block
        (
            monkeyBusinessOperator switch
            {
                "+" => Expression.Add(oldItemParameter, cexp),
                "*" => Expression.Multiply(oldItemParameter, cexp),
                _ => throw new ArgumentOutOfRangeException(monkeyBusinessOperator),
            }
        );

        public static Delegate BuildOperationExpression(Match matched)
        {
            BlockExpression block;
            var oldItemParameter = Expression.Parameter(typeof(ulong), "oldItem");

            if (matched.Groups["Factor1"].Value == "old" && matched.Groups["Factor2"].Value == "old")
            {
                block = BuildExpressionBody(matched.Groups["Operator"].Value, oldItemParameter, oldItemParameter);
            }
            else
            {
                var constantValue = matched.Groups["Factor1"].Value != "old"
                    ? ulong.Parse(matched.Groups["Factor1"].Value)
                    : ulong.Parse(matched.Groups["Factor2"].Value);

                block = BuildExpressionBody(matched.Groups["Operator"].Value, oldItemParameter, Expression.Constant(constantValue));
            }

            return Expression.Lambda(block, oldItemParameter).Compile();
        }

        public static Delegate BuildTestExpression(string input)
        {
            var match = MatchTestCondition(input);

            Func<Expression, Expression, BinaryExpression> exp = match.Groups["Operation"].Value switch
            {
                "divisible" => Expression.Modulo,
                _ => throw new ArgumentOutOfRangeException(match.Groups["Operation"].Value)
            };

            return BuildTestExpression(exp, ulong.Parse(match.Groups["Factor"].Value));
        }

        public static Delegate BuildTestExpression(Func<Expression, Expression, BinaryExpression> expression, ulong denominator)
        {
            var numeratorParameter = Expression.Parameter(typeof(ulong), "oldItemNumerator");
            var denominatorConstant = Expression.Constant(denominator);

            var block = Expression.Equal(Expression.Constant(0UL), expression(numeratorParameter, denominatorConstant));
            var lambda = Expression.Lambda(block, numeratorParameter);
            return lambda.Compile();
        }

        public static IEnumerable<ulong> ParseStartingItems(string input) =>
            MatchStartingItems(input)
                .Groups["Items"]
                .Captures
                .Select(x => ulong.Parse(x.Value));
    }
}
