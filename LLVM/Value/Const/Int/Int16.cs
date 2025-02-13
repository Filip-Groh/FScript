using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt16 : IIntConst<ConstInt16Pointer>, IValue<IValue> {
        public ConstInt16Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt16Pointer)value; }

        public ConstInt16(Int16Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt16(type.typePointer, number, isSigned);
        }

        public ConstInt16(Int16Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public ConstInt16(ConstInt16Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }
    }
}
