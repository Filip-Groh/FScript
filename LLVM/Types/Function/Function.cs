using LLVMLibrary.Type;
using LLVMLibrary.Type.Interface;
using LLVMManager.Types.Interface;

namespace LLVMManager.Types
{
    public class FunctionType {
        public IFunction functionPointer { get; init; }

        public FunctionType(IType<IType> returnType, IType<IType>[] functionParams) {
            IType[] paramArray = new IType[functionParams.Length];
            for (int i = 0; i < functionParams.Length; i++) {
                paramArray[i] = functionParams[i].typePointer;
            }

            functionPointer = TypeLibrary.CreateFunctionType(returnType.typePointer, paramArray);
        }
    }
}
