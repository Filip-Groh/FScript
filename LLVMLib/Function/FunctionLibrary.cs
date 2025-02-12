using LLVMLibrary.Module;
using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Function
{
    public unsafe struct FunctionPointer
    {
        public LLVMOpaqueValue* pointer;

        public FunctionPointer(LLVMOpaqueValue* pointer)
        {
            this.pointer = pointer;
        }
    }

    public static unsafe class FunctionLibrary
    {
        public static FunctionPointer CreateFunction(ModulePointer modulePointer, string name, IFunction functionTypePointer)
        {
            return new FunctionPointer(LLVM.AddFunction(modulePointer.pointer, Utils.StringToSByte(name), functionTypePointer.pointer));
        }
    }
}
