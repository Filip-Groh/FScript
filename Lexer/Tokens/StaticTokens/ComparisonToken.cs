namespace Lexer.Tokens.StaticTokens {
    public enum ComparisonType {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual
    }

    public class ComparisonToken : StaticToken, ITyped<ComparisonType> {
        public ComparisonType type { get; set; }

        public ComparisonToken(string text, ComparisonType type) : base(text) { 
            this.type = type;
        }
    }
}
