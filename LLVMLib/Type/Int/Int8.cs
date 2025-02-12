using LLVMLibrary.Context;
using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Type
{
    public unsafe struct TypeInt8Pointer : IInt {
        public LLVMOpaqueType* pointer { get; set; }

        public TypeInt8Pointer(LLVMOpaqueType* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class TypeLibrary {
        public static TypeInt8Pointer GetInt8Type(ContextPointer contextPointer) {
            return new TypeInt8Pointer(LLVM.Int8TypeInContext(contextPointer.pointer));
        }
    }
}
