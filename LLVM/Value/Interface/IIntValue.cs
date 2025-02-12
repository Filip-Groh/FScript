using LLVMLibrary.Value.Interface;

namespace LLVMManager.Value.Interface {
    public interface IIntValue<T> : IValue<T> where T : IIntValue;
}
