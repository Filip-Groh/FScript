using LLVMLibrary.Type.Interface;

namespace LLVMManager.Types.Interface
{
    public interface IInt<T> : IType<T> where T : IInt;
}
