using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class ValueInt8 : IIntValue<ValueInt8Pointer>, IValue<IValue> {
        public ValueInt8Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt8Pointer)value; }

        public ValueInt8() {

        }

        public ValueInt8(ValueInt8Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public ValueInt8(Int8Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt8(type.typePointer, number, isSigned);
        }

        public ValueInt8(Int8Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public static implicit operator ValueInt8(ConstInt8 constValue) {
            return new ValueInt8(constValue.valuePointer);
        }
    }
}
