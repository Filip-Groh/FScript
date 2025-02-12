using LLVMLibrary.Type;
using LLVMLibrary.Type.Interface;
using LLVMManager.Types.Interface;

namespace LLVMManager.Types
{
    public class Int64Type : IInt<TypeInt64Pointer>, IType<IType> {
        public TypeInt64Pointer typePointer { get; init; }
        public Context context { get; init; }
        IType IType<IType>.typePointer { get => typePointer; init => typePointer = (TypeInt64Pointer)value; }

        public Int64Type(Context context) {
            this.context = context;
            typePointer = TypeLibrary.GetInt64Type(context.contextPointer);
        }
    }
}
