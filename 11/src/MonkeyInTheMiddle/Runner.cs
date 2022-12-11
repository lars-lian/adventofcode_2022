using System.Linq;

namespace MonkeyInTheMiddle
{
    public static class Runner
    {
        public static ulong Run(string[] input, int rounds, bool decreaseWorry)
        {
            var monkeys = MonkeyBusinessParser.CreateMonkeys(input);

            Run(monkeys, rounds, decreaseWorry);

            return monkeys
                .OrderByDescending(m => m.Inspections)
                .Take(2)
                .Aggregate(1UL, (acc, m) => acc * m.Inspections);
        }

        private static void Run(Monkey[] monkeys, int rounds, bool decreaseWorry)
        {
            for (int i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.HeldItems.TryDequeue(out ulong item))
                    {
                        var operatedItem = monkey.Inspect(item, decreaseWorry ? 3UL : 1UL);
                        var recipientIndex = monkey.FindRecipient(operatedItem);
                        monkeys[recipientIndex].HeldItems.Enqueue(operatedItem);
                    }
                }
            }
        }
    }
}
