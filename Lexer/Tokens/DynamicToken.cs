namespace Lexer.Tokens {
    public class DynamicToken<T> : Token {
        public T value;
        public DynamicToken(string text, T value) : base(text) {
            this.value = value;
        }
    }
}
