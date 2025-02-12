using LLVMLibrary.Type;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value.Const {
    public unsafe struct ConstInt128Pointer : IIntConst {
        public LLVMOpaqueValue* pointer { get; set; }

        public ConstInt128Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class ConstantLibrary {
        public static ConstInt128Pointer CreateConstInt128(TypeInt128Pointer intType, ulong number, int signExtend) {
            return new ConstInt128Pointer(LLVM.ConstInt(intType.pointer, number, signExtend));
        }
    }
}