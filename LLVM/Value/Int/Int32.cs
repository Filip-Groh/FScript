using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class ValueInt32 : IIntValue<ValueInt32Pointer>, IValue<IValue> {
        public ValueInt32Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt32Pointer)value; }

        public ValueInt32(ValueInt32Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ValueInt32(ConstInt32 constValue) {
            return new ValueInt32(constValue.valuePointer);
        }

        public static implicit operator ValueInt32(Instruction<ValueInt32Pointer> instructionValue) {
            return new ValueInt32(instructionValue.valuePointer);
        }

        public static implicit operator ValueInt32(Instruction<ConstInt32Pointer> instructionValue) {
            return new ValueInt32(instructionValue.valuePointer);
        }
    }
}
