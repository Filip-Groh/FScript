using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class And : Bytecode {
        public Register destination;
        public Argument source;

        public And(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public And(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"AND {destination}, {source}";
        }
    }
}
