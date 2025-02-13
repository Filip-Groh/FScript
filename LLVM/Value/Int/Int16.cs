using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class ValueInt16 : IIntValue<ValueInt16Pointer>, IValue<IValue> {
        public ValueInt16Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt16Pointer)value; }

        public ValueInt16() {

        }

        public ValueInt16(ValueInt16Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public ValueInt16(Int16Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt16(type.typePointer, number, isSigned);
        }

        public ValueInt16(Int16Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public static implicit operator ValueInt16(ConstInt16 constValue) {
            return new ValueInt16(constValue.valuePointer);
        }
    }
}
