using LLVMLibrary.Context;
using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Type
{
    public unsafe struct TypeInt32Pointer : IInt {
        public LLVMOpaqueType* pointer { get; set; }

        public TypeInt32Pointer(LLVMOpaqueType* pointer)
        {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class TypeLibrary {
        public static TypeInt32Pointer GetInt32Type(ContextPointer contextPointer)
        {
            return new TypeInt32Pointer(LLVM.Int32TypeInContext(contextPointer.pointer));
        }
    }
}
