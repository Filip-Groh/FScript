using LLVMLibrary.Builder;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Instruction {
    public static unsafe partial class InstructionLibrary {
        public static void BuildRet(BuilderPointer builderPointer, IValue value) {
            LLVM.BuildRet(builderPointer.pointer, value.pointer);
        }
    }
}
