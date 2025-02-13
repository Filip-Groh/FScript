using LLVMLibrary.Value;
using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Const;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value {
    public class ValueBool : IValue<ValueBoolPointer>, IValue<IValue> {
        public ValueBoolPointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ValueBoolPointer)value; }

        public ValueBool() {

        }

        public ValueBool(ValueBoolPointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public ValueBool(BoolType type, bool state) {
            valuePointer = ConstantLibrary.CreateConstBool(type.typePointer, state);
        }

        public static implicit operator ValueBool(ConstBool constValue) {
            return new ValueBool(constValue.valuePointer);
        }
    }
}
