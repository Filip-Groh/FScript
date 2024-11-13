using Lexer;
using Lexer.Tokens.DynamicTokens;
using Lexer.Tokens.StaticTokens;
using Parser.ASTNodes;

namespace Parser {
    public class Parser {
        public Token[] tokens;
        int currentTokenIndex;
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

        public AST Parse() {
            return Block();
        }

        AST Block() {
            List<AST> statements = new List<AST>();
            while (currentToken.GetType() != typeof(EOFToken)) {
                statements.Add(Statement());
            }
            return new BlockNode(statements.ToArray());
        }

        AST Statement() {
            if (currentToken.GetType() == typeof(KeywordToken) && ((KeywordToken)currentToken).keyword == Keywords.INT) {
                Step();

                if (currentToken.GetType() != typeof(IdentifierToken)) {
                    throw new Exception("Syntax error!");
                }

                string name = ((IdentifierToken)currentToken).value;
                Step();

                if (currentToken.GetType() != typeof(AssignToken)) {
                    throw new Exception("Syntax error!");
                }

                Step();

                AST expression = Expression();

                if (currentToken.GetType() != typeof(SemicolonToken)) {
                    throw new Exception("Syntax Error!");
                }

                Step();
                return new DeclarationNode(name, new AssignmentNode(name, expression));
            }

            if (currentToken.GetType() == typeof(IdentifierToken)) {
                string name = ((IdentifierToken)currentToken).value;
                Step();

                if (currentToken.GetType() != typeof(AssignToken)) {
                    throw new Exception("Syntax error!");
                }

                Step();

                AST expression = Expression();

                if (currentToken.GetType() != typeof(SemicolonToken)) {
                    throw new Exception("Syntax Error!");
                }

                Step();
                return new AssignmentNode(name, expression);
            }

            return Expression();
        }

        AST Expression() {
            return ConditionalOR();
        }

        AST ConditionalOR() {
            AST left = ConditionalAND();
            if (currentToken.GetType() == typeof(ConditionToken) && ((ConditionToken)currentToken).type == Lexer.Tokens.StaticTokens.ConditionType.OR) {
                Step();
                return new ConditionNode(ASTNodes.ConditionType.OR, left, ConditionalAND());
            }
            return left;
        }

        AST ConditionalAND() {
            AST left = Equality();
            if (currentToken.GetType() == typeof(ConditionToken) && ((ConditionToken)currentToken).type == Lexer.Tokens.StaticTokens.ConditionType.AND) {
                Step();
                return new ConditionNode(ASTNodes.ConditionType.AND, left, Equality());
            }
            return left;
        }

        AST Equality() {
            AST left = Relational();
            if (currentToken.GetType() == typeof(ComparisonToken) && ((ComparisonToken)currentToken).type == ComparisonType.Equal) {
                Step();
                return new ComparisonNode(Comparison.Equal, left, Relational());
            }

            if (currentToken.GetType() == typeof(ComparisonToken) && ((ComparisonToken)currentToken).type == ComparisonType.NotEqual) {
                Step();
                return new ComparisonNode(Comparison.NotEqual, left, Relational());
            }
            return left;
        }

        AST Relational() {
            AST left = Additive();
            if (currentToken.GetType() == typeof(ComparisonToken) && ((ComparisonToken)currentToken).type == ComparisonType.LessThan) {
                Step();
                return new ComparisonNode(Comparison.LessThan, left, Relational());
            }

            if (currentToken.GetType() == typeof(ComparisonToken) && ((ComparisonToken)currentToken).type == ComparisonType.LessThanOrEqual) {
                Step();
                return new ComparisonNode(Comparison.LessThanOrEqual, left, Relational());
            }

            if (currentToken.GetType() == typeof(ComparisonToken) && ((ComparisonToken)currentToken).type == ComparisonType.GreaterThan) {
                Step();
                return new ComparisonNode(Comparison.GreaterThan, left, Relational());
            }

            if (currentToken.GetType() == typeof(ComparisonToken) && ((ComparisonToken)currentToken).type == ComparisonType.GreaterThanOrEqual) {
                Step();
                return new ComparisonNode(Comparison.GreaterThanOrEqual, left, Relational());
            }
            return left;
        }

        AST Additive() {
            AST left = Multiplicative();
            while (currentToken.GetType() == typeof(OperatorToken) && (((OperatorToken)currentToken).type == OperatorType.Plus || ((OperatorToken)currentToken).type == OperatorType.Minus)) {
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
            while (currentToken.GetType() == typeof(OperatorToken) && (((OperatorToken)currentToken).type == OperatorType.Multiply || ((OperatorToken)currentToken).type == OperatorType.Divide)) {
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
                }
            }
            return left;
        }

        AST Unary() {
            if (currentToken.GetType() == typeof(OperatorToken) && ((OperatorToken)currentToken).type == OperatorType.Minus) {
                Step();
                return new UnaryOperatorNode(Primary());
            }

            return Primary(); 
        }

        AST Primary() {
            if (currentToken.GetType() == typeof(NumberToken)) {
                int value = ((NumberToken)currentToken).value;
                Step();
                return new NumberNode(value);
            }

            if (currentToken.GetType() == typeof(ParamToken) && ((ParamToken)currentToken).type == ParamType.OpenParam) {
                Step();
                AST expression = Additive();
                if (currentToken.GetType() == typeof(ParamToken) && ((ParamToken)currentToken).type == ParamType.CloseParam) {
                    Step();
                    return expression;
                }
            }

            if (currentToken.GetType() == typeof(IdentifierToken)) { 
                string name = ((IdentifierToken)currentToken).value;
                Step();
                return new IdentifierNode(name);
            }

            throw new Exception("Nothing is here!");
        }
    }
}
