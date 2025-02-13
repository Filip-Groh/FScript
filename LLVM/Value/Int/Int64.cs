using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class ValueInt64 : IIntValue<ValueInt64Pointer>, IValue<IValue> {
        public ValueInt64Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt64Pointer)value; }

        public ValueInt64() {

        }

        public ValueInt64(ValueInt64Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public ValueInt64(Int64Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt64(type.typePointer, number, isSigned);
        }

        public ValueInt64(Int64Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public static implicit operator ValueInt64(ConstInt64 constValue) {
            return new ValueInt64(constValue.valuePointer);
        }
    }
}
