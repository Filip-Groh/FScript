namespace Lexer.Tokens.StaticTokens {
    public enum ComparisonType {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual
    }

    public class ComparisonToken : StaticToken {
        public ComparisonType type;

        public ComparisonToken(string text, ComparisonType type) : base(text) { 
            this.type = type;
        }
    }
}
