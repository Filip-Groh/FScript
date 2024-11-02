﻿using Lexer;
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
            return Equality();
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

            throw new Exception("Nothing is here!");
        }
    }
}
