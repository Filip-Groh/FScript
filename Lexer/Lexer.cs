using Lexer.Tokens.DynamicTokens;
using Lexer.Tokens.StaticTokens;

namespace Lexer {
    public class Lexer {
        public string code = string.Empty;

        int currentCharIndex = -1;
        char currentChar;

        List<Token> tokens = new List<Token>();

        public Lexer(string code) {
            this.code = code;    
        }

        bool Step(int numberOfSteps = 1) {
            currentCharIndex += numberOfSteps;

            if (currentCharIndex >= code.Length) {
                currentCharIndex -= numberOfSteps;
                return false;
            }

            currentChar = code[currentCharIndex];
            return true;
        }

        public Token[] Tokenize() {
            while (Step()) {
                SkipWhitespace();

                ProcessOperator();
                ProcessParam();

                if (Char.IsAsciiLetter(currentChar)) { 
                    ProcessIdentifier();
                } else if (Char.IsAsciiDigit(currentChar)) {
                    ProcessNumber();
                } else {
                    tokens.Add(new Token(currentChar.ToString()));
                }
            }

            return tokens.ToArray();
        }

        void SkipWhitespace() {
            while (Char.IsWhiteSpace(currentChar)) {
                if (!Step()) 
                    break;
            }
        }

        void ProcessIdentifier() {
            string currentIdentifier = "";
            while (Char.IsAsciiLetterOrDigit(currentChar)) {
                currentIdentifier += currentChar;
                if (!Step()) {
                    tokens.Add(new Token(currentIdentifier));
                    return;
                }
            }

            Step(-1);
            tokens.Add(new Token(currentIdentifier));
        }

        void ProcessNumber() {
            string currentIdentifier = "";
            while (Char.IsAsciiDigit(currentChar)) {
                currentIdentifier += currentChar;
                if (!Step()) {
                    tokens.Add(new NumberToken(currentIdentifier, int.Parse(currentIdentifier)));
                    return;
                }
            }

            Step(-1);
            tokens.Add(new NumberToken(currentIdentifier, int.Parse(currentIdentifier)));
        }

        void ProcessOperator() {
            if (currentChar == '+') {
                tokens.Add(new OperatorToken("+", OperatorType.Plus));
                Step();
            } 
            
            if (currentChar == '-') {
                tokens.Add(new OperatorToken("-", OperatorType.Minus));
                Step();
            } 
            
            if (currentChar == '*') {
                tokens.Add(new OperatorToken("*", OperatorType.Multiply));
                Step();
            } 
            
            if (currentChar == '/') {
                tokens.Add(new OperatorToken("/", OperatorType.Divide));
                Step();
            }
        }

        void ProcessParam() {
            if (currentChar == '(') {
                tokens.Add(new ParamToken("(", ParamType.OpenParam));
                Step();
            } 
            
            if (currentChar == ')') {
                tokens.Add(new ParamToken(")", ParamType.CloseParam));
                Step();
            }
        }
    }
}
