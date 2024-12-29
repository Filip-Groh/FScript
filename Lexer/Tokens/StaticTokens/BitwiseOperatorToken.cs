namespace Lexer.Tokens.StaticTokens {
    public enum BitwiseOperatorType {
        AND,
        OR,
        XOR,
        LeftShift,
        RightShift
    }
    public class BitwiseOperatorToken : StaticToken, ITyped<BitwiseOperatorType> {
        public BitwiseOperatorType type { get; set; }

        public BitwiseOperatorToken(string text, BitwiseOperatorType type) : base(text) {
            this.type = type;
        }
    }
}
