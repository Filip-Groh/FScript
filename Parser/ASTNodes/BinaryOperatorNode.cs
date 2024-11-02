namespace Parser.ASTNodes {
    public enum BinaryOperation {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    public class BinaryOperatorNode : AST {
        public AST left;
        public AST right;
        public BinaryOperation operation;
        
        public BinaryOperatorNode(BinaryOperation operation, AST left, AST right) { 
            this.operation = operation;
            this.left = left;
            this.right = right;
        }

        static string GetTextOfBinaryOperation(BinaryOperation operation) {
            switch (operation) {
                case BinaryOperation.Plus:
                    return "+";
                case BinaryOperation.Minus:
                    return "-";
                case BinaryOperation.Multiply:
                    return "*";
                case BinaryOperation.Divide:
                    return "/";
                default: return "";
            }
        }

        public override string ToString() {
            return $"[{left} {GetTextOfBinaryOperation(operation)} {right}]";
        }
    }
}
