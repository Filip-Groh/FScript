using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Add : Bytecode {
        public Register destination;
        public Argument source;

        public Add(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Add(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"ADD {destination}, {source}";
        }
    }
}
