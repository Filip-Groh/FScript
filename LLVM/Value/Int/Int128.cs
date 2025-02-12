using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Int {
    public class ValueInt128 : IIntValue<ValueInt128Pointer>, IValue<IValue> {
        public ValueInt128Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt128Pointer)value; }

        public ValueInt128(ValueInt128Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ValueInt128(ConstInt128 constValue) {
            return new ValueInt128(constValue.valuePointer);
        }

        public static implicit operator ValueInt128(Instruction<ValueInt128Pointer> instructionValue) {
            return new ValueInt128(instructionValue.valuePointer);
        }

        public static implicit operator ValueInt128(Instruction<ConstInt128Pointer> instructionValue) {
            return new ValueInt128(instructionValue.valuePointer);
        }
    }
}
