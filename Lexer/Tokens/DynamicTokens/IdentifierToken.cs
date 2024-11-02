namespace Lexer.Tokens.DynamicTokens {
    public class IdentifierToken : DynamicToken<string> {
        public IdentifierToken(string name) : base(name, name) {
            
        }
    }
}
