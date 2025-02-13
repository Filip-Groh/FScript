using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt32 : IIntConst<ConstInt32Pointer>, IValue<IValue> {
        public ConstInt32Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt32Pointer)value; }

        public ConstInt32(Int32Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt32(type.typePointer, number, isSigned);
        }

        public ConstInt32(Int32Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public ConstInt32(ConstInt32Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }
    }
}
