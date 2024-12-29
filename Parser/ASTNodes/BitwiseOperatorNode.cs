namespace Parser.ASTNodes {
    public enum BitwiseOperation {
        AND,
        OR,
        XOR,
        LeftShift,
        RightShift
    }

    public class BitwiseOperatorNode : AST {
        public AST left;
        public AST right;
        public BitwiseOperation operation;

        public BitwiseOperatorNode(BitwiseOperation operation, AST left, AST right) {
            this.operation = operation;
            this.left = left;
            this.right = right;
        }

        static string GetTextOfBitwiseOperation(BitwiseOperation operation) {
            switch (operation) {
                case BitwiseOperation.AND:
                    return "&";
                case BitwiseOperation.OR:
                    return "|";
                case BitwiseOperation.XOR:
                    return "^";
                case BitwiseOperation.LeftShift:
                    return "<<";
                case BitwiseOperation.RightShift:
                    return ">>";
                default:
                    return "";
            }
        }

        public override string ToString() {
            return $"[{left} {GetTextOfBitwiseOperation(operation)} {right}]";
        }
    }
}
