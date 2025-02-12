using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value {
    public unsafe struct ValueInt1Pointer : IIntValue {
        public LLVMOpaqueValue* pointer { get; set; }

        public ValueInt1Pointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }

        public static implicit operator ValueInt1Pointer(ConstInt1Pointer constPointer) {
            return new ValueInt1Pointer(constPointer.pointer);
        }
    }
}
