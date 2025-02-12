using LLVMLibrary.Type;
using LLVMLibrary.Type.Interface;
using LLVMManager.Types.Interface;

namespace LLVMManager.Types
{
    public class Int32Type : IInt<TypeInt32Pointer>, IType<IType> {
        public TypeInt32Pointer typePointer { get; init; }
        public Context context { get; init; }
        IType IType<IType>.typePointer { get => typePointer; init => typePointer = (TypeInt32Pointer)value; }

        public Int32Type(Context context) {
            this.context = context;
            typePointer = TypeLibrary.GetInt32Type(context.contextPointer);
        }
    }
}
