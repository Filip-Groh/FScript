using Compiler;
using Compiler.Arguments;
using Compiler.Bytecodes;

namespace Interpreter {
    public class Interpreter {
        public Bytecode[] bytecode;
        Dictionary<int, int> registers;

        public Interpreter(Bytecode[] bytecode) { 
            this.bytecode = bytecode;

            registers = new Dictionary<int, int>();
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
                else
                    throw new Exception("Unknown bytecode!");
            }
        }

        public override string ToString() {
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

        int GetValueFromArgument(Argument argument) {
            if (argument.GetType() == typeof(Immediate)) { 
                return ((Immediate)argument).value;
            } else if (argument.GetType() == typeof(Register)) {
                return registers[((Register)argument).registerIndex];
            }

            throw new Exception("Wrong argument!");
        }

        void ProcessMovInstruction(Mov movInstruction) {
            registers[movInstruction.destination.registerIndex] = GetValueFromArgument(movInstruction.source);
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
    }
}
