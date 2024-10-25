using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Mov : Bytecode {
        public Register destination;
        public Argument source;

        public Mov(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Mov(Register register, Register value) {
            destination = register;
            source = value;
        }

        public override string ToString() {
            return $"MOV {destination}, {source}";
        }
    }
}
