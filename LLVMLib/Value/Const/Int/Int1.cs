using LLVMLibrary.Type;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value.Const {
    public unsafe struct ConstInt1Pointer : IIntConst {
        public LLVMOpaqueValue* pointer { get; set; }

        public ConstInt1Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class ConstantLibrary {
        public static ConstInt1Pointer CreateConstInt1(TypeInt1Pointer intType, ulong number, bool isSigned) {
            return new ConstInt1Pointer(LLVM.ConstInt(intType.pointer, number, isSigned ? 1 : 0));
        }
    }
}
