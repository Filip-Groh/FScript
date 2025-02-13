using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Value {
    public unsafe struct ValueBoolPointer : IValue {
        public LLVMOpaqueValue* pointer { get; set; }

        public ValueBoolPointer(LLVMOpaqueValue* pointer) {
            this.pointer = pointer;
        }

        public static implicit operator ValueBoolPointer(ConstBoolPointer constPointer) {
            return new ValueBoolPointer(constPointer.pointer);
        }
    }
}
