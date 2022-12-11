using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MonkeyInTheMiddle
{
    [DebuggerDisplay("Bully {Name}")]
    public class Monkey
    {
        public string Name { get; init; }
        public string[] Raw { get; init; }
        public MonkeyBusinessLogic MonkeyBusinessLogic { get; init; }
        public Queue<ulong> HeldItems { get; init; }
        public int TrueTarget { get; init; }
        public int FalseTarget { get; init; }
        public ulong Inspections { get; private set; }
        private const ulong LCD = 9699690UL;

        public ulong Inspect(ulong item, ulong worryDenominator)
        {
            Inspections++;

            var operatedItem = (ulong)MonkeyBusinessLogic.Operate.DynamicInvoke(item);

            if (worryDenominator == 1 && operatedItem > LCD)
                operatedItem %= LCD;

            return operatedItem / worryDenominator;
        }

        public int FindRecipient(ulong item) => (bool)MonkeyBusinessLogic.Test.DynamicInvoke(item) ? TrueTarget : FalseTarget;
    }
}
