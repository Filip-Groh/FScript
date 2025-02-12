using LLVMLibrary.Type;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value.Const {
    public unsafe struct ConstInt32Pointer : IIntConst {
        public LLVMOpaqueValue* pointer { get; set; }

        public ConstInt32Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class ConstantLibrary {
        public static ConstInt32Pointer CreateConstInt32(TypeInt32Pointer intType, ulong number, int signExtend) {
            return new ConstInt32Pointer(LLVM.ConstInt(intType.pointer, number, signExtend));
        }
    }
}