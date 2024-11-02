namespace Lexer.Tokens.StaticTokens
{
    public enum Keywords {
        INT
    }

    public class KeywordToken : StaticToken {
        public Keywords keyword;
        public KeywordToken(string text, Keywords keyword) : base(text) {
            this.keyword = keyword;
        }
    }
}
