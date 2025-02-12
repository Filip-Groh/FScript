using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class Instruction<T> : IValue<T> where T : IValue {
        public T valuePointer { get; init; }

        public Instruction(T valuePointer) {
            this.valuePointer = valuePointer;
        }
    }
}
