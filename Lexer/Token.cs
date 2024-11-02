namespace Lexer {
    public class Token {
        public string text;
        public Token(string text) {
            this.text = text;
        }

        public override string ToString() {
            return $"[{text}]";
        }
    }
}
