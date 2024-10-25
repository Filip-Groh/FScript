using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Div : Bytecode {
        public Register destination;
        public Argument source;

        public Div(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Div(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"DIV {destination}, {source}";
        }
    }
}
