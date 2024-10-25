namespace Compiler.Arguments {
    public class Immediate : Argument {
        public int value;
        
        public Immediate(int value) { 
            this.value = value;
        }

        public override string ToString() { 
            return value.ToString();
        }
    }
}
