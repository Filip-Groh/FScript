namespace Parser.ASTNodes {
    public class IdentifierNode : AST {
        public string name;

        public IdentifierNode(string name) {
            this.name = name;    
        }

        public override string ToString() { 
            return name;
        }
    }
}
