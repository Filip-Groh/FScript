using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value {
    public unsafe struct ValueInt128Pointer : IIntValue {
        public LLVMOpaqueValue* pointer { get; set; }

        public ValueInt128Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }

        public static implicit operator ValueInt128Pointer(ConstInt128Pointer constPointer) {
            return new ValueInt128Pointer(constPointer.pointer);
        }
    }
}
