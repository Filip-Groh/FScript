namespace Compiler.Bytecodes {
    public class Jmp : Bytecode {
        public string label;

        public Jmp(string label) : base() { 
            this.label = label;
        }

        public override string ToString() {
            return $"JMP .{label}";
        }
    }
}
