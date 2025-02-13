using LLVMLibrary.Context;
using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Type
{
    public unsafe struct TypeBoolPointer : IType {
        public LLVMOpaqueType* pointer { get; set; }

        public TypeBoolPointer(LLVMOpaqueType* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class TypeLibrary {
        public static TypeBoolPointer GetBoolType(ContextPointer contextPointer) {
            return new TypeBoolPointer(LLVM.Int1TypeInContext(contextPointer.pointer));
        }
    }
}
