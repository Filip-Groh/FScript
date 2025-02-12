using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt16 : IIntConst<ConstInt16Pointer>, IValue<IValue> {
        public ConstInt16Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt16Pointer)value; }

        public ConstInt16(Int16Type type, ulong number, int signExtend) {
            valuePointer = ConstantLibrary.CreateConstInt16(type.typePointer, number, signExtend);
        }

        public ConstInt16(ConstInt16Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ConstInt16(Instruction<ConstInt16Pointer> instructionValue) {
            return new ConstInt16(instructionValue.valuePointer);
        }
    }
}
