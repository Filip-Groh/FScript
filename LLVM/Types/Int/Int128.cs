using LLVMLibrary.Type;
using LLVMLibrary.Type.Interface;
using LLVMManager.Types.Interface;

namespace LLVMManager.Types
{
    public class Int128Type : IInt<TypeInt128Pointer>, IType<IType> {
        public TypeInt128Pointer typePointer { get; init; }
        public Context context { get; init; }
        IType IType<IType>.typePointer { get => typePointer; init => typePointer = (TypeInt128Pointer)value; }

        public Int128Type(Context context) {
            this.context = context;
            typePointer = TypeLibrary.GetInt128Type(context.contextPointer);
        }
    }
}
