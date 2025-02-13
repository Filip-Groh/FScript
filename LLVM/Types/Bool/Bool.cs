using LLVMLibrary.Type;
using LLVMLibrary.Type.Interface;
using LLVMManager.Types.Interface;

namespace LLVMManager.Types {
    public class BoolType : IType<TypeBoolPointer>, IType<IType> {
        public TypeBoolPointer typePointer { get; init; }
        public Context context { get; init; }
        IType IType<IType>.typePointer { get => typePointer; init => typePointer = (TypeBoolPointer)value; }

        public BoolType(Context context) {
            this.context = context;
            typePointer = TypeLibrary.GetBoolType(context.contextPointer);
        }
    }
}
