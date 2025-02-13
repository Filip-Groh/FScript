using LLVMLibrary.Type;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value.Const {
    public unsafe struct ConstInt8Pointer : IIntConst {
        public LLVMOpaqueValue* pointer { get; set; }

        public ConstInt8Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class ConstantLibrary {
        public static ConstInt8Pointer CreateConstInt8(TypeInt8Pointer intType, ulong number, bool isSigned) {
            return new ConstInt8Pointer(LLVM.ConstInt(intType.pointer, number, isSigned ? 1 : 0));
        }
    }
}