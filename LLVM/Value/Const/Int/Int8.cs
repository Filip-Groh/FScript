using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt8 : IIntConst<ConstInt8Pointer>, IValue<IValue> {
        public ConstInt8Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt8Pointer)value; }

        public ConstInt8(Int8Type type, ulong number, int signExtend) {
            valuePointer = ConstantLibrary.CreateConstInt8(type.typePointer, number, signExtend);
        }

        public ConstInt8(ConstInt8Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ConstInt8(Instruction<ConstInt8Pointer> instructionValue) {
            return new ConstInt8(instructionValue.valuePointer);
        }
    }
}
