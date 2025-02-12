using LLVMLibrary.Value.Interface;

namespace LLVMManager.Value.Interface
{
    public interface IConst<T> : IValue<T> where T : IConst;
}
