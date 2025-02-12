using LLVMLibrary.Context;
using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Type
{
    public unsafe struct TypeInt16Pointer : IInt {
        public LLVMOpaqueType* pointer { get; set; }

        public TypeInt16Pointer(LLVMOpaqueType* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class TypeLibrary {
        public static TypeInt16Pointer GetInt16Type(ContextPointer contextPointer) {
            return new TypeInt16Pointer(LLVM.Int16TypeInContext(contextPointer.pointer));
        }
    }
}
