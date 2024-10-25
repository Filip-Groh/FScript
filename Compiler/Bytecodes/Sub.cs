using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Sub : Bytecode {
        public Register destination;
        public Argument source;

        public Sub(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Sub(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"SUB {destination}, {source}";
        }
    }
}
