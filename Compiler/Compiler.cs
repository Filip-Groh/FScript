using Compiler.Arguments;
using Compiler.Bytecodes;
using Parser;
using Parser.ASTNodes;

namespace Compiler {
    struct NodeBytecode {
        public NodeBytecode(Bytecode[] instructions, Register outputRegister) {
            this.instructions = instructions;
            this.outputRegister = outputRegister;
        }

        public Bytecode[] instructions;
        public Register outputRegister;
    }

    public class Compiler {
        public AST abstractSyntaxTree;
        int nextFreeRegister;

        public Compiler(AST abstractSyntaxTree) {
            this.abstractSyntaxTree = abstractSyntaxTree;
            
            nextFreeRegister = 0;
        }

        public Bytecode[] CompileToBytecode() {
            NodeBytecode? bytecode = CompileNode(abstractSyntaxTree);

            if (bytecode == null) {
                throw new InvalidOperationException("Empty tree!");
            }

            return ((NodeBytecode)bytecode).instructions;
        }

        int AllocNextFreeRegister() {
            return nextFreeRegister++;
        }

        NodeBytecode? CompileNode(AST node) {
            if (node.GetType() == typeof(NumberNode)) {
                NumberNode numberNode = (NumberNode)node;

                Register register = new Register(AllocNextFreeRegister());
                Immediate immediate = new Immediate(numberNode.value);
                Mov movInstruction = new Mov(register, immediate);

                return new NodeBytecode(new Bytecode[] { movInstruction }, register);
            } else if (node.GetType() == typeof(UnaryOperatorNode)) {
                UnaryOperatorNode unaryOperatorNode = (UnaryOperatorNode)node;

                NodeBytecode? expressionBrench = CompileNode(unaryOperatorNode.expression);

                if (expressionBrench == null) {
                    throw new InvalidOperationException("Missing brench!");
                }

                NodeBytecode expression = (NodeBytecode)expressionBrench;

                Bytecode[] instructions = new Bytecode[expression.instructions.Length + 1];
                expression.instructions.CopyTo(instructions, 0);
                instructions[instructions.Length - 1] = new Neg(expression.outputRegister);

                return new NodeBytecode(instructions, expression.outputRegister);
            } else if (node.GetType() == typeof(BinaryOperatorNode)) {
                BinaryOperatorNode binaryOperatorNode = (BinaryOperatorNode)node;

                NodeBytecode? leftBranch = CompileNode(binaryOperatorNode.left);
                NodeBytecode? rightBranch = CompileNode(binaryOperatorNode.right);

                if (leftBranch == null || rightBranch == null) {
                    throw new InvalidOperationException("Missing brench!");
                }

                NodeBytecode left = (NodeBytecode)leftBranch;
                NodeBytecode right = (NodeBytecode)rightBranch;

                Bytecode[] instructions = new Bytecode[left.instructions.Length + right.instructions.Length + 1];
                left.instructions.CopyTo(instructions, 0);
                right.instructions.CopyTo(instructions, left.instructions.Length);

                switch (binaryOperatorNode.operation) {
                    case BinaryOperation.Plus:
                        instructions[instructions.Length - 1] = new Add(left.outputRegister, right.outputRegister);
                        break;
                    case BinaryOperation.Minus:
                        instructions[instructions.Length - 1] = new Sub(left.outputRegister, right.outputRegister);
                        break;
                    case BinaryOperation.Multiply:
                        instructions[instructions.Length - 1] = new Mul(left.outputRegister, right.outputRegister);
                        break;
                    case BinaryOperation.Divide:
                        instructions[instructions.Length - 1] = new Div(left.outputRegister, right.outputRegister);
                        break;
                }

                return new NodeBytecode(instructions, left.outputRegister);
            } else if (node.GetType() == typeof(ComparisonNode)) { 
                ComparisonNode comparisonNode = (ComparisonNode)node;

                NodeBytecode? leftBranch = CompileNode(comparisonNode.left);
                NodeBytecode? rightBranch = CompileNode(comparisonNode.right);

                if (leftBranch == null || rightBranch == null) {
                    throw new InvalidOperationException("Missing brench!");
                }

                NodeBytecode left = (NodeBytecode)leftBranch;
                NodeBytecode right = (NodeBytecode)rightBranch;

                Bytecode[] instructions = new Bytecode[left.instructions.Length + right.instructions.Length + 2];
                left.instructions.CopyTo(instructions, 0);
                right.instructions.CopyTo(instructions, left.instructions.Length);
                Register register = new Register(AllocNextFreeRegister());

                switch (comparisonNode.comparison) {
                    case Comparison.Equal:
                        instructions[instructions.Length - 2] = new Cmp(left.outputRegister, right.outputRegister);
                        instructions[instructions.Length - 1] = new Set(Condition.Equal, register);
                        break;
                    case Comparison.NotEqual:
                        instructions[instructions.Length - 2] = new Cmp(left.outputRegister, right.outputRegister);
                        instructions[instructions.Length - 1] = new Set(Condition.NotEqual, register);
                        break;
                    case Comparison.LessThan:
                        instructions[instructions.Length - 2] = new Cmp(left.outputRegister, right.outputRegister);
                        instructions[instructions.Length - 1] = new Set(Condition.LessThan, register);
                        break;
                    case Comparison.LessThanOrEqual:
                        instructions[instructions.Length - 2] = new Cmp(left.outputRegister, right.outputRegister);
                        instructions[instructions.Length - 1] = new Set(Condition.LessThanOrEqual, register);
                        break;
                    case Comparison.GreaterThan:
                        instructions[instructions.Length - 2] = new Cmp(left.outputRegister, right.outputRegister);
                        instructions[instructions.Length - 1] = new Set(Condition.GreaterThan, register);
                        break;
                    case Comparison.GreaterThanOrEqual:
                        instructions[instructions.Length - 2] = new Cmp(left.outputRegister, right.outputRegister);
                        instructions[instructions.Length - 1] = new Set(Condition.GreaterThanOrEqual, register);
                        break;
                }

                return new NodeBytecode(instructions, register);
            }
            return null;
        }
    }
}
