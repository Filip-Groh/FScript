using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt64 : IIntConst<ConstInt64Pointer>, IValue<IValue> {
        public ConstInt64Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt64Pointer)value; }

        public ConstInt64(Int64Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt64(type.typePointer, number, isSigned);
        }

        public ConstInt64(Int64Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public ConstInt64(ConstInt64Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }
    }
}
