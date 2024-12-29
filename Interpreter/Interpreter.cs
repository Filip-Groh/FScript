using Compiler;
using Compiler.Arguments;
using Compiler.Bytecodes;

namespace Interpreter {
    public class Interpreter {
        public Bytecode[] bytecode;

        Dictionary<int, int> registers;
        List<int> stack;

        bool zeroFlag;
        bool signFlag;
        bool overflowFlag;

        int stackBase;

        public Interpreter(Bytecode[] bytecode) { 
            this.bytecode = bytecode;

            registers = new Dictionary<int, int>();
            stack = new List<int>();
        }

        public void Execute() {
            foreach (Bytecode instruction in bytecode) {
                if (instruction.GetType() == typeof(Mov))
                    ProcessMovInstruction((Mov)instruction);
                else if (instruction.GetType() == typeof(Add))
                    ProcessAddInstruction((Add)instruction);
                else if (instruction.GetType() == typeof(Sub))
                    ProcessSubInstruction((Sub)instruction);
                else if (instruction.GetType() == typeof(Mul))
                    ProcessMulInstruction((Mul)instruction);
                else if (instruction.GetType() == typeof(Div))
                    ProcessDivInstruction((Div)instruction);
                else if (instruction.GetType() == typeof(Neg))
                    ProcessNegInstruction((Neg)instruction);
                else if (instruction.GetType() == typeof(Cmp))
                    ProcessCmpInstruction((Cmp)instruction);
                else if (instruction.GetType() == typeof(Set))
                    ProcessSetInstruction((Set)instruction);
                else if (instruction.GetType() == typeof(And))
                    ProcessAndInstruction((And)instruction);
                else if (instruction.GetType() == typeof(Or))
                    ProcessOrInstruction((Or)instruction);
                else if (instruction.GetType() == typeof(Xor)) 
                    ProcessXorInstruction((Xor)instruction);
                else if (instruction.GetType() == typeof(Sal))
                    ProcessSalInstruction((Sal)instruction);
                else if (instruction.GetType() == typeof(Sar))
                    ProcessSarInstruction((Sar)instruction);
                else
                    throw new Exception("Unknown bytecode!");
            }
        }

        string RegistersToString() {
            string result = string.Empty;
            int[] keys = registers.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++) {
                if (i == 0) {
                    result += $"#{keys[i]}: {registers[keys[i]]}";
                } else {
                    result += "\n" + $"#{keys[i]}: {registers[keys[i]]}";
                }
            }
            return result;
        }

        public override string ToString() {
            string flags = string.Empty;
            flags += $"ZeroFlag: {zeroFlag}" + "\n";
            flags += $"SignFlag: {signFlag}" + "\n";
            flags += $"OverflowFlag: {overflowFlag}" + "\n";
            return $"{flags}\n{RegistersToString()}\n{String.Join("\n", stack)}";
        }

        int GetValueFromArgument(Argument argument) {
            if (argument.GetType() == typeof(Immediate)) { 
                return ((Immediate)argument).value;
            } else if (argument.GetType() == typeof(Register)) {
                return registers[((Register)argument).registerIndex];
            } else if (argument.GetType() == typeof(Stack)) {
                return stack[stackBase + ((Stack)argument).relativeIndex];
            }

            throw new Exception("Wrong argument!");
        }

        void ProcessMovInstruction(Mov movInstruction) {
            if (movInstruction.destination.GetType() == typeof(Register)) {
                registers[((Register)movInstruction.destination).registerIndex] = GetValueFromArgument(movInstruction.source);
            } else if (movInstruction.destination.GetType() == typeof(Stack)) {
                int relativeIndex = ((Stack)movInstruction.destination).relativeIndex;

                while (stack.Count <= stackBase + relativeIndex) {
                    stack.Add(0);
                }

                stack[stackBase + relativeIndex] = GetValueFromArgument(movInstruction.source);
            }
        }

        void ProcessAddInstruction(Add addInstruction) {
            registers[addInstruction.destination.registerIndex] += GetValueFromArgument(addInstruction.source);
        }

        void ProcessSubInstruction(Sub subInstruction) {
            registers[subInstruction.destination.registerIndex] -= GetValueFromArgument(subInstruction.source);
        }

        void ProcessMulInstruction(Mul mulInstruction) {
            registers[mulInstruction.destination.registerIndex] *= GetValueFromArgument(mulInstruction.source);
        }

        void ProcessDivInstruction(Div divInstruction) {
            registers[divInstruction.destination.registerIndex] /= GetValueFromArgument(divInstruction.source);
        }

        void ProcessNegInstruction(Neg negInstruction) {
            registers[negInstruction.target.registerIndex] = -registers[negInstruction.target.registerIndex];
        }

        void ProcessCmpInstruction(Cmp cmpInstruction) {
            int value1 = GetValueFromArgument(cmpInstruction.argument1);
            int value2 = GetValueFromArgument(cmpInstruction.argument2);

            try {
                int overflowCheck = checked (value1 - value2);
                overflowFlag = false;
            } catch (OverflowException) {
                overflowFlag = true;
            }

            int comparison = value1 - value2;
            zeroFlag = comparison == 0;
            signFlag = comparison < 0;
        }

        void ProcessSetInstruction(Set setInstruction) {
            bool conditionMet = false;
            switch (setInstruction.condition) {
                case Condition.Equal:
                    conditionMet = zeroFlag;
                    break;
                case Condition.NotEqual:
                    conditionMet = !zeroFlag;
                    break;
                case Condition.LessThan:
                    conditionMet = signFlag != overflowFlag;
                    break;
                case Condition.LessThanOrEqual:
                    conditionMet = zeroFlag || signFlag != overflowFlag;
                    break;
                case Condition.GreaterThan:
                    conditionMet = zeroFlag && signFlag == overflowFlag;
                    break;
                case Condition.GreaterThanOrEqual:
                    conditionMet = signFlag == overflowFlag;
                    break;
            }
            registers[setInstruction.destination.registerIndex] = conditionMet ? 1 : 0;
        }

        void ProcessAndInstruction(And andInstruction) {
            registers[andInstruction.destination.registerIndex] &= GetValueFromArgument(andInstruction.source);
        }

        void ProcessOrInstruction(Or orInstruction) {
            registers[orInstruction.destination.registerIndex] |= GetValueFromArgument(orInstruction.source);
        }

        void ProcessXorInstruction(Xor xorInstruction) {
            registers[xorInstruction.destination.registerIndex] ^= GetValueFromArgument(xorInstruction.source);
        }

        void ProcessSalInstruction(Sal salInstruction) {
            registers[salInstruction.destination.registerIndex] <<= GetValueFromArgument(salInstruction.source);
        }

        void ProcessSarInstruction(Sar sarInstruction) {
            registers[sarInstruction.destination.registerIndex] >>= GetValueFromArgument(sarInstruction.source);
        }
    }
}
