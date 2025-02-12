using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Int {
    public class ValueInt8 : IIntValue<ValueInt8Pointer>, IValue<IValue> {
        public ValueInt8Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt8Pointer)value; }

        public ValueInt8(ValueInt8Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ValueInt8(ConstInt8 constValue) {
            return new ValueInt8(constValue.valuePointer);
        }

        public static implicit operator ValueInt8(Instruction<ValueInt8Pointer> instructionValue) {
            return new ValueInt8(instructionValue.valuePointer);
        }

        public static implicit operator ValueInt8(Instruction<ConstInt8Pointer> instructionValue) {
            return new ValueInt8(instructionValue.valuePointer);
        }
    }
}
