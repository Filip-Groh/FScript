namespace Lexer.Tokens.StaticTokens {
    public enum OperatorType {
        Plus,
        Minus,
        Multiply,
        Divide,
        Modulo
    }

    public class OperatorToken : StaticToken, ITyped<OperatorType> {
        public OperatorType type { get; set; }

        public OperatorToken(string text, OperatorType type) : base(text) {
            this.type = type;
        }
    }
}
