using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value {
    public unsafe struct ValueInt16Pointer : IIntValue {
        public LLVMOpaqueValue* pointer { get; set; }

        public ValueInt16Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }

        public static implicit operator ValueInt16Pointer(ConstInt16Pointer constPointer) {
            return new ValueInt16Pointer(constPointer.pointer);
        }
    }
}
