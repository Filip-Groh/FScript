using LLVMSharp.Interop;

namespace LLVMLibrary.Context
{
    public unsafe struct ContextPointer
    {
        public LLVMOpaqueContext* pointer;

        public ContextPointer(LLVMOpaqueContext* pointer)
        {
            this.pointer = pointer;
        }
    }

    public static unsafe class ContextLibrary
    {
        public static ContextPointer CreateContext()
        {
            return new ContextPointer(LLVM.ContextCreate());
        }

        public static void DisposeContext(ContextPointer contextPointer)
        {
            LLVM.ContextDispose(contextPointer.pointer);
        }
    }
}
