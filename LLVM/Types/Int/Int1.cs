using LLVMLibrary.Type;
using LLVMLibrary.Type.Interface;
using LLVMManager.Types.Interface;

namespace LLVMManager.Types
{
    public class Int1Type : IInt<TypeInt1Pointer>, IType<IType> {
        public TypeInt1Pointer typePointer { get; init; }
        public Context context { get; init; }
        IType IType<IType>.typePointer { get => typePointer; init => typePointer = (TypeInt1Pointer)value; }

        public Int1Type(Context context) {
            this.context = context;
            typePointer = TypeLibrary.GetInt1Type(context.contextPointer);
        }
    }
}
