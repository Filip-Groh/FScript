using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value {
    public unsafe struct ValueInt32Pointer : IIntValue {
        public LLVMOpaqueValue* pointer { get; set; }

        public ValueInt32Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }

        public static implicit operator ValueInt32Pointer(ConstInt32Pointer constPointer) {
            return new ValueInt32Pointer(constPointer.pointer);
        }
    }
}
