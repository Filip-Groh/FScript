namespace Lexer.Tokens {
    internal class DynamicToken : Token {
        public int value;
        public DynamicToken(string text, int value) : base(text) {
            this.value = value;
        }
    }
}
