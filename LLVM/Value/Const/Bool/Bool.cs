using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstBool : IConst<ConstBoolPointer>, IValue<IValue> {
        public ConstBoolPointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstBoolPointer)value; }

        public ConstBool(BoolType type, bool state) {
            valuePointer = ConstantLibrary.CreateConstBool(type.typePointer, state);
        }

        public ConstBool(ConstBoolPointer valuePointer) {
            this.valuePointer = valuePointer;
        }
    }
}
