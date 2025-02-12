using LLVMLibrary.Context;
using LLVMSharp.Interop;

namespace LLVMLibrary.Module
{
    public unsafe struct ModulePointer
    {
        public LLVMOpaqueModule* pointer;

        public ModulePointer(LLVMOpaqueModule* pointer)
        {
            this.pointer = pointer;
        }
    }

    public static unsafe class ModuleLibrary
    {
        public static ModulePointer CreateModule(ContextPointer contextPointer, string name)
        {
            return new ModulePointer(LLVM.ModuleCreateWithNameInContext(Utils.StringToSByte(name), contextPointer.pointer));
        }

        public static void DisposeModule(ModulePointer modulePointer)
        {
            LLVM.DisposeModule(modulePointer.pointer);
        }

        public static string PrintModuleToString(ModulePointer modulePointer)
        {
            sbyte* ptr = LLVM.PrintModuleToString(modulePointer.pointer);
            string str = Utils.SByteToString(ptr);
            LLVM.DisposeMessage(ptr);
            return str;
        }
    }
}
