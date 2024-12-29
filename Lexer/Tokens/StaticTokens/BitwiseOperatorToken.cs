namespace Lexer.Tokens.StaticTokens {
    public enum BitwiseOperatorType {
        AND,
        OR,
        XOR,
        LeftShift,
        RightShift
    }
    public class BitwiseOperatorToken : StaticToken {
        public BitwiseOperatorType type;

        public BitwiseOperatorToken(string text, BitwiseOperatorType type) : base(text) {
            this.type = type;
        }
    }
}
