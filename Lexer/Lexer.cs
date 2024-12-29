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

                if (currentChar == ';') {
                    tokens.Add(new SemicolonToken());
                    continue;
                }



                if (currentChar == '(') {
                    tokens.Add(new ParamToken("(", ParamType.OpenParam));
                    continue;
                }
                if (currentChar == ')') {
                    tokens.Add(new ParamToken(")", ParamType.CloseParam));
                    continue;
                }



                if (currentChar == '+') {
                    tokens.Add(new OperatorToken("+", OperatorType.Plus));
                    continue;
                } 
                if (currentChar == '-') {
                    tokens.Add(new OperatorToken("-", OperatorType.Minus));
                    continue;
                } 
                if (currentChar == '*') {
                    tokens.Add(new OperatorToken("*", OperatorType.Multiply));
                    continue;
                } 
                if (currentChar == '/') {
                    tokens.Add(new OperatorToken("/", OperatorType.Divide));
                    continue;
                } 
                if (currentChar == '%') {
                    tokens.Add(new OperatorToken("%", OperatorType.Modulo));
                    continue;
                }



                if (currentChar == '<') {
                    Step();
                    if (currentChar == '=') {
                        tokens.Add(new ComparisonToken("<=", ComparisonType.LessThanOrEqual));
                    } else if (currentChar == '<') {
                        tokens.Add(new BitwiseOperatorToken("<<", BitwiseOperatorType.LeftShift));
                    } else {
                        tokens.Add(new ComparisonToken("<", ComparisonType.LessThan));
                        Step(-1);
                    }
                    continue;
                } 
                if (currentChar == '>') {
                    Step();
                    if (currentChar == '=') {
                        tokens.Add(new ComparisonToken(">=", ComparisonType.GreaterThanOrEqual));
                    } else if (currentChar == '>') {
                        tokens.Add(new BitwiseOperatorToken(">>", BitwiseOperatorType.RightShift));
                    } else {
                        tokens.Add(new ComparisonToken(">", ComparisonType.GreaterThan));
                        Step(-1);
                    }
                    continue;
                }
                if (currentChar == '=') {
                    Step();
                    if (currentChar == '=') {
                        tokens.Add(new ComparisonToken("==", ComparisonType.Equal));
                    } else {
                        tokens.Add(new AssignToken("="));
                        Step(-1);
                    }
                    continue;
                } 
                if (currentChar == '!') {
                    Step();
                    if (currentChar == '=') {
                        tokens.Add(new ComparisonToken("!=", ComparisonType.NotEqual));
                    } else {
                        tokens.Add(new ConditionToken("!", ConditionType.NOT));
                        Step(-1);
                    }
                    continue;
                } 


                 
                if (currentChar == '&') {
                    Step();
                    if (currentChar == '&') {
                        tokens.Add(new ConditionToken("&&", ConditionType.AND));
                    } else {
                        tokens.Add(new BitwiseOperatorToken("&", BitwiseOperatorType.AND));
                        Step(-1);
                    }
                    continue;
                } 
                if (currentChar == '|') {
                    Step();
                    if (currentChar == '|') {
                        tokens.Add(new ConditionToken("||", ConditionType.OR));
                    } else {
                        tokens.Add(new BitwiseOperatorToken("|", BitwiseOperatorType.OR));
                        Step(-1);
                    }
                    continue;
                }
                if (currentChar == '^') {
                    tokens.Add(new BitwiseOperatorToken("^", BitwiseOperatorType.XOR));
                    continue;
                }



                if (Char.IsAsciiLetter(currentChar)) {
                    ProcessIdentifier();
                } else if (Char.IsAsciiDigit(currentChar)) {
                    ProcessNumber();
                } else {
                    if (!Char.IsWhiteSpace(currentChar)) {
                        tokens.Add(new Token(currentChar.ToString()));
                    }
                }
            }

            tokens.Add(new EOFToken());

            return tokens.ToArray();
        }

        void SkipWhitespace() {
            while (Char.IsWhiteSpace(currentChar)) {
                if (!Step()) 
                    break;
            }
        }

        void AddKeywordOrIdentifierToken(string currentIdentifier) {
            switch (currentIdentifier) {
                case "int":
                    tokens.Add(new KeywordToken(currentIdentifier, Keywords.INT));
                    break;
                default:
                    tokens.Add(new IdentifierToken(currentIdentifier));
                    break;
            }
        }

        void ProcessIdentifier() {
            string currentIdentifier = "";
            while (Char.IsAsciiLetterOrDigit(currentChar)) {
                currentIdentifier += currentChar;
                if (!Step()) {
                    AddKeywordOrIdentifierToken(currentIdentifier);
                    return;
                }
            }

            Step(-1);
            AddKeywordOrIdentifierToken(currentIdentifier);
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
    }
}