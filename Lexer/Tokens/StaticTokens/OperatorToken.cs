namespace Lexer.Tokens.StaticTokens {
    public enum OperatorType {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    public class OperatorToken : StaticToken {
        public OperatorType type;

        public OperatorToken(string text, OperatorType type) : base(text) {
            this.type = type;
        }
    }
}
