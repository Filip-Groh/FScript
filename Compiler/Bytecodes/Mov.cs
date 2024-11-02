using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Mov : Bytecode {
        public Argument destination;
        public Argument source;

        public Mov(Register register, Argument value) {
            destination = register;
            source = value;
        }

        public Mov(Stack stack, Argument value) {
            destination = stack;
            source = value;
        }

        public override string ToString() {
            return $"MOV {destination}, {source}";
        }
    }
}
