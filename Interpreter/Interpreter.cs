using Compiler;
using Compiler.Arguments;
using Compiler.Bytecodes;
using Lexer;

namespace Interpreter {
    public class Interpreter {
        public Bytecode[] bytecode;
        Bytecode instruction;

        Dictionary<int, int> registers;
        List<int> stack;
        Dictionary<string, int> labels;

        bool zeroFlag;
        bool signFlag;
        bool overflowFlag;

        int stackBase;
        int programCounter;

        public Interpreter(Bytecode[] bytecode) { 
            this.bytecode = bytecode;
            instruction = bytecode[0];

            stackBase = 0;
            programCounter = -1;

            registers = new Dictionary<int, int>();
            stack = new List<int>();
            labels = new Dictionary<string, int>();

            LoadLabels();
        }

        bool Step(int numberOfSteps = 1) {
            programCounter += numberOfSteps;

            if (programCounter >= bytecode.Length) {
                programCounter -= numberOfSteps;
                return false;
            }

            instruction = bytecode[programCounter];
            return true;
        }

        public void Execute() {
            while (Step()) {
                if (instruction.GetType() == typeof(Label))
                    continue;
                else if (instruction.GetType() == typeof(Mov))
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
                else if (instruction.GetType() == typeof(J))
                    ProcessJInstruction((J)instruction);
                else if (instruction.GetType() == typeof(Jmp))
                    ProcessJmpInstruction((Jmp)instruction);
                else
                    throw new Exception("Unknown bytecode!");
            }
        }

        void LoadLabels() {
            for (int i = 0; i < bytecode.Length; i++) {
                if (bytecode[i].GetType() != typeof(Label))
                    continue;

                Label label = (Label)bytecode[i];
                labels.Add(label.name, i);
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
            string flagsString = "Flags:" + "\n";
            flagsString += $"ZeroFlag: {zeroFlag}" + "\n";
            flagsString += $"SignFlag: {signFlag}" + "\n";
            flagsString += $"OverflowFlag: {overflowFlag}" + "\n";

            string registersString = "Registers:" + "\n";
            registersString += RegistersToString() + "\n";

            string stackString = "Stack:" + "\n";
            stackString += String.Join("\n", stack);

            return $"{flagsString}\n{registersString}\n{stackString}";
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

        bool ResolveCondition(Condition condition) {
            bool conditionMet = false;
            switch (condition) {
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
            return conditionMet;
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
            if (divInstruction.remainderDestination != null) {
                registers[divInstruction.remainderDestination.registerIndex] = registers[divInstruction.destination.registerIndex] % GetValueFromArgument(divInstruction.source);
            }
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
            registers[setInstruction.destination.registerIndex] = ResolveCondition(setInstruction.condition) ? 1 : 0;
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

        void ProcessJInstruction(J jInstruction) {
            if (!ResolveCondition(jInstruction.condition))
                return;

            if (!labels.TryGetValue(jInstruction.label, out int address)) {
                throw new InvalidOperationException("Jumping to unknown label");
            }

            programCounter = address;
        }

        void ProcessJmpInstruction(Jmp jmpInstruction) {
            if (!labels.TryGetValue(jmpInstruction.label, out int address)) {
                throw new InvalidOperationException("Jumping to unknown label");
            }

            programCounter = address;
        }
    }
}
