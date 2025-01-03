﻿namespace Parser.ASTNodes {
    public class NumberNode : AST {
        public int value;

        public NumberNode(int value) { 
            this.value = value;
        }

        public override string ToString() { 
            return value.ToString();
        }
    }
}
