using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const
{
    public class ConstInt32 : IIntConst<ConstInt32Pointer>, IValue<IValue>
    {
        public ConstInt32Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt32Pointer)value; }

        public ConstInt32(Int32Type type, ulong number, int signExtend)
        {
            valuePointer = ConstantLibrary.CreateConstInt32(type.typePointer, number, signExtend);
        }

        public ConstInt32(ConstInt32Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }

        public static implicit operator ConstInt32(Instruction<ConstInt32Pointer> instructionValue) {
            return new ConstInt32(instructionValue.valuePointer);
        }
    }
}
