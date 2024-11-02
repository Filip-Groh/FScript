using Compiler.Arguments;

namespace Compiler.Bytecodes {
    public class Neg : Bytecode {
        public Register target;
        
        public Neg(Register target) { 
            this.target = target;
        }

        public override string ToString() {
            return $"NEG {target}";
        }
    }
}
