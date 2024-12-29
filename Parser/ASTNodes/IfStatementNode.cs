namespace Parser.ASTNodes {
    public class IfStatementNode : AST {
        public AST condition;
        public AST block;
        public AST? elseBlock;

        public IfStatementNode(AST condition, AST block, AST? elseBlock = null) : base() { 
            this.condition = condition;
            this.block = block;
            this.elseBlock = elseBlock;
        }

        public override string ToString() {
            if (elseBlock != null)
                return $"if ({condition}) {{\n{block}\n}} else {{\n{elseBlock}\n}}";

            return $"if ({condition}) {{\n{block}\n}}";
        }
    }
}
