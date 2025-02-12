namespace LLVMManager {
    public abstract class Disposable : IDisposable {
        bool isDisposed = false;

        ~Disposable() {
            Dispose();
        }

        public void Dispose() {
            if (isDisposed) return;

            OnDispose();

            isDisposed = true;
        }

        public abstract void OnDispose();
    }
}
