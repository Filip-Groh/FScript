using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public enum Condition {
        Equal,
        NotEqual,
        LessThan, 
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
    }

    public class Set : Bytecode {
        public Condition condition;
        public Register destination;

        public Set(Condition condition, Register destination) { 
            this.condition = condition;
            this.destination = destination;
        }

        static string GetTextOfCondition(Condition condition) {
            switch (condition) {
                case Condition.Equal:
                    return "e";
                case Condition.NotEqual:
                    return "ne";
                case Condition.LessThan:
                    return "l";
                case Condition.LessThanOrEqual:
                    return "le";
                case Condition.GreaterThan:
                    return "g";
                case Condition.GreaterThanOrEqual:
                    return "ge";
                default:
                    return "";
            }
        }

        public override string ToString() {
            return $"SET${GetTextOfCondition(condition)} {destination}";
        }
    }
}
