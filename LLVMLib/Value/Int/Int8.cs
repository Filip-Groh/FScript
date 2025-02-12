using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value {
    public unsafe struct ValueInt8Pointer : IIntValue {
        public LLVMOpaqueValue* pointer { get; set; }

        public ValueInt8Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }

        public static implicit operator ValueInt8Pointer(ConstInt8Pointer constPointer) {
            return new ValueInt8Pointer(constPointer.pointer);
        }
    }
}
