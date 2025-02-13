using LLVMLibrary.Value.Const;
using LLVMLibrary.Value.Interface;
using LLVMManager.Types;
using LLVMManager.Value.Interface;

namespace LLVMManager.Value.Const {
    public class ConstInt128 : IIntConst<ConstInt128Pointer>, IValue<IValue> {
        public ConstInt128Pointer valuePointer { get; init; }
        IValue IValue<IValue>.valuePointer { get => valuePointer; init => valuePointer = (ConstInt128Pointer)value; }

        public ConstInt128(Int128Type type, ulong number, bool isSigned = false) {
            valuePointer = ConstantLibrary.CreateConstInt128(type.typePointer, number, isSigned);
        }

        public ConstInt128(Int128Type type, long number) : this(type, (ulong)number, number < 0) {

        }

        public ConstInt128(ConstInt128Pointer valuePointer) {
            this.valuePointer = valuePointer;
        }
    }
}
