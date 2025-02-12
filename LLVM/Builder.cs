using LLVMLibrary.Builder;
using LLVMLibrary.Instruction;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value;
using LLVMManager.Value.Interface;

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

        public void BuildRet(IValue<IValue> returnValue) {
            InstructionLibrary.BuildRet(builderPointer, returnValue.valuePointer);
        }

        public Instruction<T> BuildAdd<T>(IIntValue<T> lhs, IIntValue<T> rhs, string name) where T : struct, IIntValue {
            return new Instruction<T>(InstructionLibrary.BuildAdd(builderPointer, lhs.valuePointer, rhs.valuePointer, name));
        }
    }
}
