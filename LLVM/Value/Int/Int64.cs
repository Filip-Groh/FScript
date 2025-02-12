using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Int {
    public class ValueInt64 : IIntValue<ValueInt64Pointer>, IValue<IValue> {
        public ValueInt64Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt64Pointer)value; }

        public ValueInt64(ValueInt64Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ValueInt64(ConstInt64 constValue) {
            return new ValueInt64(constValue.valuePointer);
        }

        public static implicit operator ValueInt64(Instruction<ValueInt64Pointer> instructionValue) {
            return new ValueInt64(instructionValue.valuePointer);
        }

        public static implicit operator ValueInt64(Instruction<ConstInt64Pointer> instructionValue) {
            return new ValueInt64(instructionValue.valuePointer);
        }
    }
}
