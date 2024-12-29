using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Sal : Bytecode {
        public Register destination;
        public Argument source;

        public Sal(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Sal(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"SAL {destination}, {source}";
        }
    }
}
