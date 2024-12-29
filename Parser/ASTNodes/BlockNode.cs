namespace Parser.ASTNodes {
    public class BlockNode : AST {
        public AST[] statements;
        public int indent;

        public BlockNode(AST[] statements, int indent) {
            this.statements = statements;
            this.indent = indent;
        }

        public override string ToString() {
            return Utils.ArrayToString<AST>.ToString(statements, "\n", indent);
        }
    }
}
