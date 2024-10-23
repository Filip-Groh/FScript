namespace Lexer {
    public class Token {
        public string text;
        public Token(string text) {
            this.text = text;
        }

        public override string ToString() {
            return $"[{text}]";
        }

        public static string ArrayToString(Token[] tokens) {
            string result = string.Empty;
            for (int i = 0; i < tokens.Length; i++) {
                if (i == 0) {
                    result += tokens[i];
                } else {
                    result += ", " + tokens[i];
                }
            }

            return result;
        }
    }
}
