namespace Parser.ASTNodes {
    public class DeclarationNode : AST {
        public string name;
        public AssignmentNode? inicializationNode;

        public DeclarationNode(string name, AssignmentNode? inicializationNode = null) { 
            this.name = name;
            this.inicializationNode = inicializationNode;
        }

        public override string ToString() {
            string inicialization = inicializationNode is not null ? inicializationNode.ToString(true) : string.Empty;
            return $"[int {name}{inicialization}]";
        }
    }
}
