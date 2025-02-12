using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt128 : IIntConst<ConstInt128Pointer>, IValue<IValue> {
        public ConstInt128Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt128Pointer)value; }

        public ConstInt128(Int128Type type, ulong number, int signExtend) {
            valuePointer = ConstantLibrary.CreateConstInt128(type.typePointer, number, signExtend);
        }

        public ConstInt128(ConstInt128Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ConstInt128(Instruction<ConstInt128Pointer> instructionValue) {
            return new ConstInt128(instructionValue.valuePointer);
        }
    }
}
