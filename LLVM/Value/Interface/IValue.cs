using LLVMLibrary.Value.Interface;

namespace LLVMManager.Value.Interface {
    public interface IValue<T> where T : IValue {
        T valuePointer { get; init; }
    }
}
