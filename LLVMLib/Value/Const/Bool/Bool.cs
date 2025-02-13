using LLVMLibrary.Type;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value.Const {
    public unsafe struct ConstBoolPointer : IConst {
        public LLVMOpaqueValue* pointer { get; set; }

        public ConstBoolPointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class ConstantLibrary {
        public static ConstBoolPointer CreateConstBool(TypeBoolPointer intType, bool state) {
            return new ConstBoolPointer(LLVM.ConstInt(intType.pointer, (ulong)(state ? 1 : 0), 0));
        }
    }
}
