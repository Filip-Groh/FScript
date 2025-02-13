using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class ValueInt1 : IIntValue<ValueInt1Pointer>, IValue<IValue> {
        public ValueInt1Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueInt1Pointer)value; }

        public ValueInt1() {

        }

        public ValueInt1(ValueInt1Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public ValueInt1(Int1Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt1(type.typePointer, number, isSigned);
        }

        public ValueInt1(Int1Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public static implicit operator ValueInt1(ConstInt1 constValue) {
            return new ValueInt1(constValue.valuePointer);
        }
    }
}
