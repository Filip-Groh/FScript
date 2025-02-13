using LLVMLibrary.BasicBlock;

namespace LLVMManager
{
    public partial class BasicBlock {
        public BasicBlockPointer basicBlockPointer { get; init; }
        public string name { get; init; }

        Builder builder;

        public BasicBlock(Context context, string name) {
            this.name = name;

            basicBlockPointer = BasicBlockLibrary.CreateBasicBlock(context.contextPointer, name);

            builder = new Builder(context);
            builder.SetPosition(this);
        }
    }
}
