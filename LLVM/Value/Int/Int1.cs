using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Int {
    public class ValueInt1 : IIntValue<ValueInt1Pointer>, IValue<IValue> {
        public ValueInt1Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt1Pointer)value; }

        public ValueInt1(ValueInt1Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ValueInt1(ConstInt1 constValue) {
            return new ValueInt1(constValue.valuePointer);
        }

        public static implicit operator ValueInt1(Instruction<ValueInt1Pointer> instructionValue) {
            return new ValueInt1(instructionValue.valuePointer);
        }

        public static implicit operator ValueInt1(Instruction<ConstInt1Pointer> instructionValue) {
            return new ValueInt1(instructionValue.valuePointer);
        }
    }
}
