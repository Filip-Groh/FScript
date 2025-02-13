using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class ValueInt128 : IIntValue<ValueInt128Pointer>, IValue<IValue> {
        public ValueInt128Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt128Pointer)value; }

        public ValueInt128() {

        }

        public ValueInt128(ValueInt128Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public ValueInt128(Int128Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt128(type.typePointer, number, isSigned);
        }

        public ValueInt128(Int128Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public static implicit operator ValueInt128(ConstInt128 constValue) {
            return new ValueInt128(constValue.valuePointer);
        }
    }
}
