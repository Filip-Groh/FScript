using LLVMLibrary.Type;
using LLVMLibrary.Type.Interface;
using LLVMManager.Types.Interface;

namespace LLVMManager.Types
{
    public class Int16Type : IInt<TypeInt16Pointer>, IType<IType> {
        public TypeInt16Pointer typePointer { get; init; }
        public Context context { get; init; }
        IType IType<IType>.typePointer { get => typePointer; init => typePointer = (TypeInt16Pointer)value; }

        public Int16Type(Context context) {
            this.context = context;
            typePointer = TypeLibrary.GetInt16Type(context.contextPointer);
        }
    }
}
