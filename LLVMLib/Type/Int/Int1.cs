using LLVMLibrary.Context;
using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Type
{
    public unsafe struct TypeInt1Pointer : IInt {
        public LLVMOpaqueType* pointer { get; set; }

        public TypeInt1Pointer(LLVMOpaqueType* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class TypeLibrary {
        public static TypeInt1Pointer GetInt1Type(ContextPointer contextPointer) {
            return new TypeInt1Pointer(LLVM.Int1TypeInContext(contextPointer.pointer));
        }
    }
}
