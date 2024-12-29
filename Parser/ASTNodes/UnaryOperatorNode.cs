namespace Parser.ASTNodes {
    public enum UnaryOperationType {
        Negation,
        Not
    }

    public class UnaryOperatorNode : AST {
        public UnaryOperationType operation;
        public AST expression;

        public UnaryOperatorNode(UnaryOperationType operation, AST expression) { 
            this.operation = operation;
            this.expression = expression;
        }

        static string GetTextOfUnaryOperation(UnaryOperationType operation) {
            switch (operation) {
                case UnaryOperationType.Negation:
                    return "-";
                case UnaryOperationType.Not:
                    return "!";
                default:
                    return "";
            }
        }

        public override string ToString() {
            return $"[{GetTextOfUnaryOperation(operation)}{expression}]";
        }
    }
}
