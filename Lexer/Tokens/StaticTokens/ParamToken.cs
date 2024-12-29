namespace Lexer.Tokens.StaticTokens {
    public enum BracketType {
        OpenParentheses,
        CloseParentheses,
        OpenCurlyBraces,
        CloseCurlyBraces,
    }

    public class BracketToken : StaticToken, ITyped<BracketType> {
        public BracketType type { get; set; }

        public BracketToken(string text, BracketType type) : base(text) {
            this.type = type;
        }
    }
}
