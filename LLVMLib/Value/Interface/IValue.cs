using LLVMSharp.Interop;

namespace LLVMLibrary.Value.Interface
{
    public unsafe interface IValue
    {
        LLVMOpaqueValue* pointer { get; set; }
    }
}
