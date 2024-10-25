namespace Compiler.Arguments {
    public class Register : Argument {
        public int registerIndex;

        public Register(int registerIndex) { 
            this.registerIndex = registerIndex;
        }

        public override string ToString() {
            return $"#{registerIndex}";
        }
    }
}
