using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt1 : IIntConst<ConstInt1Pointer>, IValue<IValue> {
        public ConstInt1Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt1Pointer)value; }

        public ConstInt1(Int1Type type, ulong number, int signExtend) {
            valuePointer = ConstantLibrary.CreateConstInt1(type.typePointer, number, signExtend);
        }

        public ConstInt1(ConstInt1Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ConstInt1(Instruction<ConstInt1Pointer> instructionValue) {
            return new ConstInt1(instructionValue.valuePointer);
        }
    }
}
