namespace Lexer.Tokens.StaticTokens {
    public enum ConditionType {
        AND,
        OR,
        NOT
    }

    public class ConditionToken : StaticToken, ITyped<ConditionType> {
        public ConditionType type { get; set; }

        public ConditionToken(string text, ConditionType type) : base(text) {
            this.type = type;
        }
    }
}