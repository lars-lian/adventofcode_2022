using System;

namespace MonkeyInTheMiddle
{
    public record MonkeyBusinessLogic
    {
        public int TargetMonkeyTrue { get; init; }
        public int TargetMonkeyFalse { get; init; }
        public Delegate Test { get; init; }
        public Delegate Operate { get; init; }
    }
}
