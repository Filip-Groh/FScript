using LLVMLibrary.Type;
using LLVMLibrary.Type.Interface;
using LLVMManager.Types.Interface;

namespace LLVMManager.Types
{
    public class Int8Type : IInt<TypeInt8Pointer>, IType<IType> {
        public TypeInt8Pointer typePointer { get; init; }
        public Context context { get; init; }
        IType IType<IType>.typePointer { get => typePointer; init => typePointer = (TypeInt8Pointer)value; }

        public Int8Type(Context context) {
            this.context = context;
            typePointer = TypeLibrary.GetInt8Type(context.contextPointer);
        }
    }
}