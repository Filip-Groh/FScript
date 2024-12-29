using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Div : Bytecode {
        public Register destination;
        public Register? remainderDestination;
        public Argument source;

        public Div(Register register, Immediate value) {
            destination = register;
            source = value;
        }

        public Div(Register register, Register value) {
            destination = register;
            source = value;
        }

        public Div(Register register, Register remainderRegister, Immediate value) {
            destination = register;
            remainderDestination = remainderRegister;
            source = value;
        }

        public Div(Register register, Register remainderRegister, Register value) {
            destination = register;
            remainderDestination = remainderRegister;
            source = value;
        }

        public override string ToString() {
            if (remainderDestination != null) {
                return $"DIV {destination}, {remainderDestination}, {source}";    
            }

            return $"DIV {destination}, {source}";
        }
    }
}
