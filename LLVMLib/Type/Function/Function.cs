using LLVMLibrary.Type.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Type
{
    public unsafe struct FunctionTypePointer<T> : IFunction {
        public LLVMOpaqueType* pointer { get; set; }

        public FunctionTypePointer(LLVMOpaqueType* pointer)
        {
            this.pointer = pointer;
        }
    }

    public static unsafe partial class TypeLibrary {
        public static FunctionTypePointer<T> CreateFunctionType<T>(T returnTypePointer, IType[] functionParams) where T : IType {
            LLVMOpaqueType*[] paramArray = new LLVMOpaqueType*[functionParams.Length];
            for (int i = 0; i < functionParams.Length; i++)
            {
                paramArray[i] = functionParams[i].pointer;
            }

            fixed (LLVMOpaqueType** paramArrayPointer = paramArray)
            {
                return new FunctionTypePointer<T>(LLVM.FunctionType(returnTypePointer.pointer, paramArrayPointer, (uint)paramArray.Length, 0));
            }
        }
    }
}
