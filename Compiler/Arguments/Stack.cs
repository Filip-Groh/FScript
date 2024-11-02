namespace Compiler.Arguments {
    public class Stack : Argument {
        public int relativeIndex;

        public Stack(int relativeIndex) { 
            this.relativeIndex = relativeIndex;
        }

        public override string ToString() {
            return $"[base - {relativeIndex}]";
        }
    }
}
