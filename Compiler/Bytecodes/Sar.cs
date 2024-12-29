using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Sar : Bytecode {
        public Register destination;
        public Argument source;

        public Sar(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Sar(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"SAR {destination}, {source}";
        }
    }
}
