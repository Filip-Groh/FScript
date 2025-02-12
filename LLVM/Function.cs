using LLVMLibrary.BasicBlock;
using LLVMLibrary.Function;
using LLVMManager.Types;

namespace LLVMManager
{
    public class Function {
        public FunctionPointer functionPointer { get; init; }

        public Function(Context context, Module module, string name, FunctionType functionType) {
            functionPointer = FunctionLibrary.CreateFunction(module.modulePointer, name, functionType.functionPointer);
        }

        public void AppendBasicBlock(BasicBlock basicBlock) {
            BasicBlockLibrary.AppendExistingBasicBlock(functionPointer, basicBlock.basicBlockPointer);
        }
    }
}
