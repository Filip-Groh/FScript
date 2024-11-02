namespace Parser.ASTNodes {
    public enum Comparison {
        Equal,
        NotEqual, 
        LessThan, 
        LessThanOrEqual, 
        GreaterThan, 
        GreaterThanOrEqual
    }
    
    public class ComparisonNode : AST {
        public AST left;
        public AST right;
        public Comparison comparison;

        public ComparisonNode(Comparison comparison, AST left, AST right) {
            this.left = left;
            this.right = right;
            this.comparison = comparison;
        }

        static string GetTextOfComparison(Comparison comparison) {
            switch (comparison) {
                case Comparison.Equal:
                    return "==";
                case Comparison.NotEqual:
                    return "!=";
                case Comparison.LessThan:
                    return "<";
                case Comparison.LessThanOrEqual:
                    return "<=";
                case Comparison.GreaterThan:
                    return ">";
                case Comparison.GreaterThanOrEqual:
                    return ">=";
                default:
                    return "";
            }
        }

        public override string ToString() {
            return $"[{left} {GetTextOfComparison(comparison)} {right}]";
        }
    }
}
