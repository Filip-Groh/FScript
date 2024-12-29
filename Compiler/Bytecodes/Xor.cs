using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Xor : Bytecode {
        public Register destination;
        public Argument source;

        public Xor(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Xor(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"XOR {destination}, {source}";
        }
    }
}
