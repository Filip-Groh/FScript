using LLVMLibrary.Instruction;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Interface;

namespace LLVMManager {
    public partial class BasicBlock {
        public BasicBlock BuildShl<T, TOut>(out TOut instruction, IIntValue<T> lhs, IIntValue<T> rhs, string name) where T : struct, IIntValue where TOut : IIntValue<T>, new() {
            instruction = new TOut() { valuePointer = InstructionLibrary.BuildShl(builder.builderPointer, lhs.valuePointer, rhs.valuePointer, name) };
            return this;
        }
    }
}