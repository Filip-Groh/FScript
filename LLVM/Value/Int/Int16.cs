using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Int {
    public class ValueInt16 : IIntValue<ValueInt16Pointer>, IValue<IValue> {
        public ValueInt16Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt16Pointer)value; }

        public ValueInt16(ValueInt16Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ValueInt16(ConstInt16 constValue) {
            return new ValueInt16(constValue.valuePointer);
        }

        public static implicit operator ValueInt16(Instruction<ValueInt16Pointer> instructionValue) {
            return new ValueInt16(instructionValue.valuePointer);
        }

        public static implicit operator ValueInt16(Instruction<ConstInt16Pointer> instructionValue) {
            return new ValueInt16(instructionValue.valuePointer);
        }
    }
}
