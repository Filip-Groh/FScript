namespace Lexer.Tokens.StaticTokens
{
    public enum KeywordType {
        INT,
        IF,
        ELSE
    }

    public class KeywordToken : StaticToken, ITyped<KeywordType> {
        public KeywordType type {  get; set; }

        public KeywordToken(string text, KeywordType keyword) : base(text) {
            this.type = keyword;
        }
    }
}
