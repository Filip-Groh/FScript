namespace Lexer.Tokens.StaticTokens {
    public enum ConditionType {
        AND,
        OR
    }

    public class ConditionToken : StaticToken {
        public ConditionType type;

        public ConditionToken(string text, ConditionType type) : base(text) {
            this.type = type;
        }
    }
}