using LLVMLibrary.Type.Interface;

namespace LLVMManager.Types.Interface
{
    public interface IType<T> where T : IType
    {
        Context context { get; init; }
        T typePointer { get; init; }
    }
}
