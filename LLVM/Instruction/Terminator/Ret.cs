using LLVMLibrary.Instruction;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Interface;

namespace LLVMManager {
    public partial class BasicBlock {
        public void BuildRet(IValue<IValue> returnValue) {
            InstructionLibrary.BuildRet(builder.builderPointer, returnValue.valuePointer);
        }
    }
}
