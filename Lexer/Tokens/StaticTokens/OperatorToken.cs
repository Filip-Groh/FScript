namespace Lexer.Tokens.StaticTokens {
    public enum OperatorType {
        Plus,
        Minus,
        Multiply,
        Divide
    }

    internal class OperatorToken : StaticToken {
        OperatorType type;

        public OperatorToken(string text, OperatorType type) : base(text) {
            this.type = type;
        }
    }
}
