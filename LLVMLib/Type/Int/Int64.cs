using LLVMLibrary.Context;
using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Type
{
    public unsafe struct TypeInt64Pointer : IInt {
        public LLVMOpaqueType* pointer { get; set; }

        public TypeInt64Pointer(LLVMOpaqueType* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class TypeLibrary {
        public static TypeInt64Pointer GetInt64Type(ContextPointer contextPointer) {
            return new TypeInt64Pointer(LLVM.Int64TypeInContext(contextPointer.pointer));
        }
    }
}
