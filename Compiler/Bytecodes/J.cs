namespace Compiler.Bytecodes {
    public class J : Bytecode {
        public Condition condition;
        public string label;

        public J(Condition condition, string label) : base() {
            this.condition = condition;
            this.label = label;
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
            return $"J${GetTextOfCondition(condition)} .{label}";
        }
    }
}
