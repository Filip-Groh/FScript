using LLVMLibrary.Type;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value.Const {
    public unsafe struct ConstInt16Pointer : IIntConst {
        public LLVMOpaqueValue* pointer { get; set; }

        public ConstInt16Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class ConstantLibrary {
        public static ConstInt16Pointer CreateConstInt16(TypeInt16Pointer intType, ulong number, int signExtend) {
            return new ConstInt16Pointer(LLVM.ConstInt(intType.pointer, number, signExtend));
        }
    }
}