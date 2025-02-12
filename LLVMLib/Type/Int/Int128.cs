using LLVMLibrary.Context;
using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Type
{
    public unsafe struct TypeInt128Pointer : IInt {
        public LLVMOpaqueType* pointer { get; set; }

        public TypeInt128Pointer(LLVMOpaqueType* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class TypeLibrary {
        public static TypeInt128Pointer GetInt128Type(ContextPointer contextPointer) {
            return new TypeInt128Pointer(LLVM.Int128TypeInContext(contextPointer.pointer));
        }
    }
}
