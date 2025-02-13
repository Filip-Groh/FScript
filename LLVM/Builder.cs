using LLVMLibrary.Builder;

namespace LLVMManager
{
    public class Builder : Disposable {
        public BuilderPointer builderPointer { get; init; }

        public Builder(Context context) {
            builderPointer = BuilderLibrary.CreateBuilder(context.contextPointer);
        }
        public override void OnDispose() {
            BuilderLibrary.DisposeBuilder(builderPointer);
        }

        public void SetPosition(BasicBlock basicBlock) {
            BuilderLibrary.PositionBuilderAtEnd(builderPointer, basicBlock.basicBlockPointer);
        }
    }
}
