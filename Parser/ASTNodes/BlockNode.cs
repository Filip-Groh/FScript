namespace Parser.ASTNodes {
    public class BlockNode : AST {
        public AST[] statements;

        public BlockNode(AST[] statements) {
            this.statements = statements;
        }

        public override string ToString() {
            return Utils.ArrayToString<AST>.ToString(statements, "\n");
        }
    }
}
