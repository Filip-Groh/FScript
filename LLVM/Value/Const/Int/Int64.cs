using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt64 : IIntConst<ConstInt64Pointer>, IValue<IValue> {
        public ConstInt64Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt64Pointer)value; }

        public ConstInt64(Int64Type type, ulong number, int signExtend) {
            valuePointer = ConstantLibrary.CreateConstInt64(type.typePointer, number, signExtend);
        }

        public ConstInt64(ConstInt64Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ConstInt64(Instruction<ConstInt64Pointer> instructionValue) {
            return new ConstInt64(instructionValue.valuePointer);
        }
    }
}
