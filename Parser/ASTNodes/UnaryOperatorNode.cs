namespace Parser.ASTNodes {
    public class UnaryOperatorNode : AST {
        public AST expression;

        public UnaryOperatorNode(AST expression) { 
            this.expression = expression;
        }

        public override string ToString() {
            return $"[-{expression}]";
        }
    }
}
