using LLVMLibrary.Type;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value.Const {
    public unsafe struct ConstInt64Pointer : IIntConst {
        public LLVMOpaqueValue* pointer { get; set; }

        public ConstInt64Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class ConstantLibrary {
        public static ConstInt64Pointer CreateConstInt64(TypeInt64Pointer intType, ulong number, int signExtend) {
            return new ConstInt64Pointer(LLVM.ConstInt(intType.pointer, number, signExtend));
        }
    }
}