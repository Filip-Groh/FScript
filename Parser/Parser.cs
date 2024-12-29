using Lexer;
using Lexer.Tokens;
using Lexer.Tokens.DynamicTokens;
using Lexer.Tokens.StaticTokens;
using Parser.ASTNodes;

namespace Parser {
    public class Parser {
        public Token[] tokens;
        int currentTokenIndex;
        int indent = 0;
        Token currentToken;

        public Parser(Token[] tokens) {
            this.tokens = tokens;

            currentTokenIndex = 0;
            currentToken = tokens[0];
        }

        bool Step(int numberOfSteps = 1) {
            currentTokenIndex += numberOfSteps;

            if (currentTokenIndex >= tokens.Length) {
                currentTokenIndex -= numberOfSteps;
                return false;
            }

            currentToken = tokens[currentTokenIndex];
            return true;
        }

        bool IsCurrentToken<Token>() where Token : Lexer.Token {
            return currentToken.GetType() == typeof(Token);
        }

        bool IsCurrentTokenOfType<Token, Type>(Type type) where Token : Lexer.Token, ITyped<Type> where Type : Enum {
            return IsCurrentToken<Token>() && ((Token)currentToken).type.Equals(type);
        }

        bool IsCurrentTokenOfType<Token, Type>(Type[] types) where Token : Lexer.Token, ITyped<Type> where Type : Enum {
            return types.Any(type => IsCurrentTokenOfType<Token, Type>(type));
        }

        public AST Parse() {
            return Block();
        }

        AST Block() {
            List<AST> statements = new List<AST>();
            while (!(IsCurrentToken<EOFToken>() || IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.CloseCurlyBraces))) {
                statements.Add(Statement());
            }
            return new BlockNode(statements.ToArray(), indent);
        }

        AST Statement() {
            if (IsCurrentTokenOfType<KeywordToken, KeywordType>(KeywordType.IF)) {
                Step();
                return IfStatement();
            }

            if (IsCurrentTokenOfType<KeywordToken, KeywordType>(KeywordType.INT)) {
                Step();
                
                if (!IsCurrentToken<IdentifierToken>()) {
                    throw new Exception("Syntax error!");
                }

                string name = ((IdentifierToken)currentToken).value;
                Step();
                
                if (!IsCurrentToken<AssignToken>()) {
                    throw new Exception("Syntax error!");
                }

                Step();

                AST expression = Expression();
                
                if (!IsCurrentToken<SemicolonToken>()) {
                    throw new Exception("Syntax Error!");
                }

                Step();
                return new DeclarationNode(name, new AssignmentNode(name, expression));
            }
            
            if (IsCurrentToken<IdentifierToken>()) {
                string name = ((IdentifierToken)currentToken).value;
                Step();

                if (!IsCurrentToken<AssignToken>()) {
                    throw new Exception("Syntax error!");
                }

                Step();

                AST expression = Expression();

                if (!IsCurrentToken<SemicolonToken>()) {
                    throw new Exception("Syntax Error!");
                }

                Step();
                return new AssignmentNode(name, expression);
            }

            return Expression();
        }

        AST IfStatement() {
            if (!IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.OpenParentheses)) {
                throw new Exception("Syntax error!");
            }

            Step();

            AST expression = Expression();

            if (!IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.CloseParentheses)) {
                throw new Exception("Syntax error!");
            }

            Step();

            if (!IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.OpenCurlyBraces)) {
                throw new Exception("Syntax error!");
            }

            Step();

            indent++;

            AST block = Block();

            if (!IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.CloseCurlyBraces)) {
                throw new Exception("Syntax error!");
            }

            Step();
            indent--;

            if (!IsCurrentTokenOfType<KeywordToken, KeywordType>(KeywordType.ELSE)) {
                return new IfStatementNode(expression, block);
            }

            Step();

            if (!IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.OpenCurlyBraces)) {
                throw new Exception("Syntax error!");
            }

            Step();

            indent++;

            AST elseBlock = Block();

            if (!IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.CloseCurlyBraces)) {
                throw new Exception("Syntax error!");
            }

            Step();
            indent--;
            return new IfStatementNode(expression, block, elseBlock);
        }

        AST Expression() {
            return ConditionalOR();
        }

        AST ConditionalOR() {
            AST left = ConditionalAND();
            if (IsCurrentTokenOfType<ConditionToken, Lexer.Tokens.StaticTokens.ConditionType>(Lexer.Tokens.StaticTokens.ConditionType.OR)) {
                Step();
                return new ConditionNode(ASTNodes.ConditionType.OR, left, ConditionalAND());
            }
            return left;
        }

        AST ConditionalAND() {
            AST left = BitwiseOR();
            if (IsCurrentTokenOfType<ConditionToken, Lexer.Tokens.StaticTokens.ConditionType>(Lexer.Tokens.StaticTokens.ConditionType.AND)) {
                Step();
                return new ConditionNode(ASTNodes.ConditionType.AND, left, BitwiseOR());
            }
            return left;
        }

        AST BitwiseOR() {
            AST left = BitwiseXOR();
            if (IsCurrentTokenOfType<BitwiseOperatorToken, BitwiseOperatorType>(BitwiseOperatorType.OR)) {
                Step();
                return new BitwiseOperatorNode(BitwiseOperation.OR, left, BitwiseXOR());
            }
            return left;
        }

        AST BitwiseXOR() {
            AST left = BitwiseAND();
            if (IsCurrentTokenOfType<BitwiseOperatorToken, BitwiseOperatorType>(BitwiseOperatorType.XOR)) {
                Step();
                return new BitwiseOperatorNode(BitwiseOperation.XOR, left, BitwiseAND());
            }
            return left;
        }

        AST BitwiseAND() {
            AST left = Equality();
            if (IsCurrentTokenOfType<BitwiseOperatorToken, BitwiseOperatorType>(BitwiseOperatorType.AND)) {
                Step();
                return new BitwiseOperatorNode(BitwiseOperation.AND, left, Equality());
            }
            return left;
        }

        AST Equality() {
            AST left = Relational();
            if (IsCurrentTokenOfType<ComparisonToken, ComparisonType>(ComparisonType.Equal)) {
                Step();
                return new ComparisonNode(Comparison.Equal, left, Relational());
            }

            if (IsCurrentTokenOfType<ComparisonToken, ComparisonType>(ComparisonType.NotEqual)) {
                Step();
                return new ComparisonNode(Comparison.NotEqual, left, Relational());
            }
            return left;
        }

        AST Relational() {
            AST left = Shift();
            while (IsCurrentTokenOfType<ComparisonToken, ComparisonType>([ComparisonType.LessThan, ComparisonType.LessThanOrEqual, ComparisonType.GreaterThan, ComparisonType.GreaterThanOrEqual])) {
                ComparisonToken currentComparisonToken = (ComparisonToken)currentToken;
                switch (currentComparisonToken.type) {
                    case ComparisonType.LessThan:
                        Step();
                        left = new ComparisonNode(Comparison.LessThan, left, Shift());
                        break;
                    case ComparisonType.LessThanOrEqual:
                        Step();
                        left = new ComparisonNode(Comparison.LessThanOrEqual, left, Shift());
                        break;
                    case ComparisonType.GreaterThan:
                        Step();
                        left = new ComparisonNode(Comparison.GreaterThan, left, Shift());
                        break;
                    case ComparisonType.GreaterThanOrEqual:
                        Step();
                        left = new ComparisonNode(Comparison.GreaterThanOrEqual, left, Shift());
                        break;
                }
            }
            return left;
        }

        AST Shift() {
            AST left = Additive();
            while (IsCurrentTokenOfType<BitwiseOperatorToken, BitwiseOperatorType>([BitwiseOperatorType.LeftShift, BitwiseOperatorType.RightShift])) {
                BitwiseOperatorToken currentOperatorToken = (BitwiseOperatorToken)currentToken;
                switch (currentOperatorToken.type) {
                    case BitwiseOperatorType.LeftShift:
                        Step();
                        left = new BitwiseOperatorNode(BitwiseOperation.LeftShift, left, Additive());
                        break;
                    case BitwiseOperatorType.RightShift:
                        Step();
                        left = new BitwiseOperatorNode(BitwiseOperation.RightShift, left, Additive());
                        break;
                }
            }
            return left;
        }

        AST Additive() {
            AST left = Multiplicative();
            while (IsCurrentTokenOfType<OperatorToken, OperatorType>([OperatorType.Plus, OperatorType.Minus])) {
                OperatorToken currentOperatorToken = (OperatorToken)currentToken;
                switch (currentOperatorToken.type) {
                    case OperatorType.Plus:
                        Step();
                        left = new BinaryOperatorNode(BinaryOperation.Plus, left, Multiplicative());
                        break;
                    case OperatorType.Minus:
                        Step();
                        left = new BinaryOperatorNode(BinaryOperation.Minus, left, Multiplicative());
                        break;
                }
            }
            return left;
        }

        AST Multiplicative() {
            AST left = Unary();
            while (IsCurrentTokenOfType<OperatorToken, OperatorType>([OperatorType.Multiply, OperatorType.Divide, OperatorType.Modulo])) {
                OperatorToken currentOperatorToken = (OperatorToken)currentToken;
                switch (currentOperatorToken.type) {
                    case OperatorType.Multiply:
                        Step();
                        left = new BinaryOperatorNode(BinaryOperation.Multiply, left, Unary());
                        break;
                    case OperatorType.Divide:
                        Step();
                        left = new BinaryOperatorNode(BinaryOperation.Divide, left, Unary());
                        break;
                    case OperatorType.Modulo:
                        Step();
                        left = new BinaryOperatorNode(BinaryOperation.Modulo, left, Unary());
                        break;
                }
            }
            return left;
        }

        AST Unary() {
            bool isNOT = false;
            while (IsCurrentTokenOfType<ConditionToken, Lexer.Tokens.StaticTokens.ConditionType>(Lexer.Tokens.StaticTokens.ConditionType.NOT)) {
                isNOT = !isNOT;
                Step();
            }

            if (isNOT) {
                return new UnaryOperatorNode(UnaryOperationType.Not, Primary());
            }

            bool isNEG = false;
            while (IsCurrentTokenOfType<OperatorToken, OperatorType>(OperatorType.Minus)) {
                isNEG = !isNEG;
                Step();
            }

            if (isNEG) {
                return new UnaryOperatorNode(UnaryOperationType.Negation, Primary());
            }

            return Primary(); 
        }

        AST Primary() {
            if (IsCurrentToken<NumberToken>()) {
                int value = ((NumberToken)currentToken).value;
                Step();
                return new NumberNode(value);
            }
            
            if (IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.OpenParentheses)) {
                Step();
                AST expression = Expression();
                if (IsCurrentTokenOfType<BracketToken, BracketType>(BracketType.CloseParentheses)) {
                    Step();
                    return expression;
                }
            }
            
            if (IsCurrentToken<IdentifierToken>()) { 
                string name = ((IdentifierToken)currentToken).value;
                Step();
                return new IdentifierNode(name);
            }

            throw new Exception("Nothing is here!");
        }
    }
}
