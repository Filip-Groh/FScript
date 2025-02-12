using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value {
    public unsafe struct ValueInt64Pointer : IIntValue {
        public LLVMOpaqueValue* pointer { get; set; }

        public ValueInt64Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }

        public static implicit operator ValueInt64Pointer(ConstInt64Pointer constPointer) {
            return new ValueInt64Pointer(constPointer.pointer);
        }
    }
}
