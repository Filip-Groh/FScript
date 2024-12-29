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
        int nextFreeStack;

        public Compiler(AST abstractSyntaxTree) {
            this.abstractSyntaxTree = abstractSyntaxTree;
            
            nextFreeRegister = 0;
            nextFreeStack = 0;
        }

        public Bytecode[] CompileToBytecode() {
            NodeBytecode? bytecode = CompileNode(abstractSyntaxTree, new Dictionary<string, int>());

            if (bytecode == null) {
                throw new InvalidOperationException("Empty tree!");
            }

            return ((NodeBytecode)bytecode).instructions;
        }

        int AllocNextFreeRegister() {
            return nextFreeRegister++;
        }

        int AllocNextFreeStack() {
            return nextFreeStack++;
        }

        NodeBytecode? CompileNode(AST node, Dictionary<string, int> variables) {
            if (node.GetType() == typeof(BlockNode)) {
                BlockNode blockNode = (BlockNode)node;
                List<Bytecode> blocks = new List<Bytecode>();

                foreach (AST statement in blockNode.statements) {
                    NodeBytecode? statementBytecode = CompileNode(statement, variables);

                    if (statementBytecode != null) {
                        blocks.AddRange(statementBytecode.Value.instructions);
                    }
                }

                return new NodeBytecode(blocks.ToArray(), new Register(0));
            } else if (node.GetType() == typeof(NumberNode)) {
                NumberNode numberNode = (NumberNode)node;

                Register register = new Register(AllocNextFreeRegister());
                Immediate immediate = new Immediate(numberNode.value);
                Mov movInstruction = new Mov(register, immediate);

                return new NodeBytecode(new Bytecode[] { movInstruction }, register);
            } else if (node.GetType() == typeof(IdentifierNode)) {
                IdentifierNode identifierNode = (IdentifierNode)node;

                bool exists = variables.TryGetValue(identifierNode.name, out int relativeStackIndex);
                if (!exists) {
                    throw new Exception("Variable doesn't exists!");
                }

                Register register = new Register(AllocNextFreeRegister());
                Stack stack = new Stack(relativeStackIndex);
                Mov movInstruction = new Mov(register, stack);

                return new NodeBytecode(new Bytecode[] { movInstruction }, register);
            } else if (node.GetType() == typeof(UnaryOperatorNode)) {
                UnaryOperatorNode unaryOperatorNode = (UnaryOperatorNode)node;

                NodeBytecode? expressionBrench = CompileNode(unaryOperatorNode.expression, variables);

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

                NodeBytecode? leftBrench = CompileNode(binaryOperatorNode.left, variables);
                NodeBytecode? rightBrench = CompileNode(binaryOperatorNode.right, variables);

                if (leftBrench == null || rightBrench == null) {
                    throw new InvalidOperationException("Missing brench!");
                }

                NodeBytecode left = (NodeBytecode)leftBrench;
                NodeBytecode right = (NodeBytecode)rightBrench;

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
            } else if (node.GetType() == typeof(BitwiseOperatorNode)) {
                BitwiseOperatorNode bitwiseOperatorNode = (BitwiseOperatorNode)node;

                NodeBytecode? leftBrench = CompileNode(bitwiseOperatorNode.left, variables);
                NodeBytecode? rightBrench = CompileNode(bitwiseOperatorNode.right, variables);

                if (leftBrench == null || rightBrench == null) {
                    throw new InvalidOperationException("Missing brench!");
                }

                NodeBytecode left = (NodeBytecode)leftBrench;
                NodeBytecode right = (NodeBytecode)rightBrench;

                Bytecode[] instructions = new Bytecode[left.instructions.Length + right.instructions.Length + 1];
                left.instructions.CopyTo(instructions, 0);
                right.instructions.CopyTo(instructions, left.instructions.Length);

                switch (bitwiseOperatorNode.operation) {
                    case BitwiseOperation.AND:
                        instructions[instructions.Length - 1] = new And(left.outputRegister, right.outputRegister);
                        break;
                    case BitwiseOperation.OR:
                        instructions[instructions.Length - 1] = new Or(left.outputRegister, right.outputRegister);
                        break;
                    case BitwiseOperation.XOR:
                        instructions[instructions.Length - 1] = new Xor(left.outputRegister, right.outputRegister);
                        break;
                    case BitwiseOperation.LeftShift:
                        instructions[instructions.Length - 1] = new Sal(left.outputRegister, right.outputRegister);
                        break;
                    case BitwiseOperation.RightShift:
                        instructions[instructions.Length - 1] = new Sar(left.outputRegister, right.outputRegister);
                        break;
                }

                return new NodeBytecode(instructions, left.outputRegister);
            } else if (node.GetType() == typeof(ComparisonNode)) {
                ComparisonNode comparisonNode = (ComparisonNode)node;

                NodeBytecode? leftBrench = CompileNode(comparisonNode.left, variables);
                NodeBytecode? rightBrench = CompileNode(comparisonNode.right, variables);

                if (leftBrench == null || rightBrench == null) {
                    throw new InvalidOperationException("Missing brench!");
                }

                NodeBytecode left = (NodeBytecode)leftBrench;
                NodeBytecode right = (NodeBytecode)rightBrench;

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
            } else if (node.GetType() == typeof(ConditionNode)) {
                ConditionNode conditionNode = (ConditionNode)node;

                NodeBytecode? leftBrench = CompileNode(conditionNode.left, variables);
                NodeBytecode? rightBrench = CompileNode(conditionNode.right, variables);

                if (leftBrench == null || rightBrench == null) {
                    throw new InvalidOperationException("Missing brench!");
                }

                NodeBytecode left = (NodeBytecode)leftBrench;
                NodeBytecode right = (NodeBytecode)rightBrench;

                Bytecode[] instructions = new Bytecode[left.instructions.Length + right.instructions.Length + 1];
                left.instructions.CopyTo(instructions, 0);
                right.instructions.CopyTo(instructions, left.instructions.Length);

                switch (conditionNode.condition) {
                    case ConditionType.AND:
                        instructions[instructions.Length - 1] = new And(left.outputRegister, right.outputRegister);
                        break;
                    case ConditionType.OR:
                        instructions[instructions.Length - 1] = new Or(left.outputRegister, right.outputRegister);
                        break;
                }

                return new NodeBytecode(instructions, left.outputRegister);
            } else if (node.GetType() == typeof(DeclarationNode)) {
                DeclarationNode declarationNode = (DeclarationNode)node;

                variables.Add(declarationNode.name, AllocNextFreeStack());

                if (declarationNode.inicializationNode is not null) {
                    return CompileNode(declarationNode.inicializationNode, variables);
                }
            } else if (node.GetType() == typeof(AssignmentNode)) { 
                AssignmentNode assignmentNode = (AssignmentNode)node;
                bool exists = variables.TryGetValue(assignmentNode.name, out int relativeStackIndex);
                if (!exists) {
                    throw new Exception("Variable doesn't exists!");
                }

                NodeBytecode? expressionBrench = CompileNode(assignmentNode.expression, variables);

                if (expressionBrench == null) {
                    throw new InvalidOperationException("Missing expression!");
                }

                NodeBytecode expression = (NodeBytecode)expressionBrench;

                Bytecode[] instructions = new Bytecode[expression.instructions.Length + 1];
                expression.instructions.CopyTo(instructions, 0);
                Stack stack = new Stack(relativeStackIndex);

                instructions[instructions.Length - 1] = new Mov(stack, expression.outputRegister);
                return new NodeBytecode(instructions, expression.outputRegister);
            }
            return null;
        }
    }
}
