namespace Parser.ASTNodes {
    public class AssignmentNode : AST {
        public string name;
        public AST expression;

        public AssignmentNode(string name, AST expression) { 
            this.name = name;
            this.expression = expression;
        }

        public override string ToString() {
            return ToString(false);
        }

        public string ToString(bool isInDeclaration) {
            if (isInDeclaration) {
                return $" = {expression}";
            } 
            return $"[{name} = {expression}]";
        }
    }
}
