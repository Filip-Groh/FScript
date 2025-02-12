using LLVMLibrary.BasicBlock;

namespace LLVMManager
{
    public class BasicBlock {
        public BasicBlockPointer basicBlockPointer { get; init; }
        public string name { get; init; }

        public BasicBlock(Context context, string name) {
            this.name = name;

            basicBlockPointer = BasicBlockLibrary.CreateBasicBlock(context.contextPointer, name);
        }
    }
}
