using LLVMSharp.Interop;

namespace LLVMLibrary.Type.Interface
{
    public unsafe interface IType
    {
        LLVMOpaqueType* pointer { get; set; }
    }
}
