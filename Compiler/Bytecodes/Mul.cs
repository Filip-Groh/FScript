using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Mul : Bytecode {
        public Register destination;
        public Argument source;

        public Mul(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Mul(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"MUL {destination}, {source}";
        }
    }
}
