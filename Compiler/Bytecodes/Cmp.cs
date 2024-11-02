using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Cmp : Bytecode {
        public Argument argument1;
        public Argument argument2;

        public Cmp(Immediate value1, Immediate value2) {
            argument1 = value1;
            argument2 = value2;
        }

        public Cmp(Immediate value, Register register) {
            argument1 = value;
            argument2 = register;
        }

        public Cmp(Register register, Immediate value) {
            argument1 = register;
            argument2 = value;
        }

        public Cmp(Register register1, Register register2) {
            argument1 = register1;
            argument2 = register2;
        }

        public override string ToString() {
            return $"CMP {argument1}, {argument2}";
        }
    }
}
