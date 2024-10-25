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

        public static string ArrayToString(Bytecode[] bytecode) {
            string result = string.Empty;
            for (int i = 0; i < bytecode.Length; i++) {
                if (i == 0) {
                    result += bytecode[i];
                } else {
                    result += "\n" + bytecode[i];
                }
            }

            return result;
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
            } else if (node.GetType() == typeof(BinaryOperatorNode)) {
                BinaryOperatorNode binaryOperatorNode = (BinaryOperatorNode)node;

                NodeBytecode? leftBranch = CompileNode(binaryOperatorNode.left);
                NodeBytecode? lrightBranch = CompileNode(binaryOperatorNode.right);

                if (leftBranch == null || lrightBranch == null) {
                    throw new InvalidOperationException("Missing brench!");
                }

                NodeBytecode left = (NodeBytecode)leftBranch;
                NodeBytecode right = (NodeBytecode)lrightBranch;

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
            }

            return null;
        }
    }
}
