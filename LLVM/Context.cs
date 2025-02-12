using LLVMLibrary.Context;

namespace LLVMManager
{
    public class Context : Disposable {
        public ContextPointer contextPointer { get; init; }

        public Context() {
            contextPointer = ContextLibrary.CreateContext();
        }

        public override void OnDispose() {
            ContextLibrary.DisposeContext(contextPointer);
        }
    }
}
