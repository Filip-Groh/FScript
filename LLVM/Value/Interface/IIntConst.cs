using LLVMLibrary.Value.Interface;

namespace LLVMManager.Value.Interface
{
    public interface IIntConst<T> : IConst<T>, IIntValue<T> where T : IIntConst;
}
