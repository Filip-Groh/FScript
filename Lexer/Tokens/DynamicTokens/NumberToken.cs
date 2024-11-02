namespace Lexer.Tokens.DynamicTokens {
    public class NumberToken : DynamicToken<int> {
        public NumberToken(string text, int value) : base(text, value) {

        }
    }
}
