using LLVMLibrary.Context;
using LLVMLibrary.Function;
using LLVMSharp.Interop;

namespace LLVMLibrary.BasicBlock
{
    public unsafe struct BasicBlockPointer
    {
        public LLVMOpaqueBasicBlock* pointer;

        public BasicBlockPointer(LLVMOpaqueBasicBlock* pointer)
        {
            this.pointer = pointer;
        }
    }

    public static unsafe class BasicBlockLibrary
    {
        public static BasicBlockPointer CreateBasicBlock(ContextPointer contextPointer, string name)
        {
            return new BasicBlockPointer(LLVM.CreateBasicBlockInContext(contextPointer.pointer, Utils.StringToSByte(name)));
        }

        public static void AppendExistingBasicBlock(FunctionPointer functionPointer, BasicBlockPointer basicBlockPointer)
        {
            LLVM.AppendExistingBasicBlock(functionPointer.pointer, basicBlockPointer.pointer);
        }
    }
}
