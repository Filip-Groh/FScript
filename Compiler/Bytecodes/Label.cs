namespace Compiler.Bytecodes {
    public class Label : Bytecode {
        public string name;

        public Label(string name) : base() { 
            this.name = name;
        }

        public override string ToString() {
            return $"{name}: ";
        }
    }
}
