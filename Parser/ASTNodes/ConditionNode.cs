namespace Parser.ASTNodes {
    public enum ConditionType {
        AND,
        OR
    }

    public class ConditionNode : AST {
        public AST left;
        public AST right;
        public ConditionType condition;

        public ConditionNode(ConditionType condition, AST left, AST right) { 
            this.condition = condition;
            this.left = left;
            this.right = right;
        }

        static string GetStringOfCondition(ConditionType condition) {
            switch (condition) {
                case ConditionType.AND:
                    return "AND";
                case ConditionType.OR:
                    return "OR";
                default:
                    return "";
            }
        }

        public override string ToString() {
            return $"[{left} {GetStringOfCondition(condition)} {right}]";
        }
    }
}
