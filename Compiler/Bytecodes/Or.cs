using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Or : Bytecode {
        public Register destination;
        public Argument source;

        public Or(Register register, Immediate value) { 
            destination = register;
            source = value;
        }

        public Or(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"OR {destination}, {source}";
        }
    }
}
