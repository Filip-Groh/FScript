namespace Lexer.Tokens.StaticTokens {
    public enum ParamType {
        OpenParam,
        CloseParam
    }

    internal class ParamToken : StaticToken {
        public ParamType type;

        public ParamToken(string text, ParamType type) : base(text) {
            this.type = type;
        }
    }
}
