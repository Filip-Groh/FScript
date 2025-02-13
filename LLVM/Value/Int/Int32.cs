using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class ValueInt32 : IIntValue<ValueInt32Pointer>, IValue<IValue> {
        public ValueInt32Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt32Pointer)value; }

        public ValueInt32() {

        }

        public ValueInt32(ValueInt32Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public ValueInt32(Int32Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt32(type.typePointer, number, isSigned);
        }

        public ValueInt32(Int32Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public static implicit operator ValueInt32(ConstInt32 constValue) {
            return new ValueInt32(constValue.valuePointer);
        }
    }
}
