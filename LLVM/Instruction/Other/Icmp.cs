using LLVMLibrary.Instruction;
using LLVMLibrary.Value.Interface;
using LLVMManager.Value;
using LLVMManager.Value.Interface;

namespace LLVMManager {
    public partial class BasicBlock {
        public enum ComparePredicate {
            EQ = 32,
            NE = 33,
            UGT = 34,
            UGE = 35,
            ULT = 36,
            ULE = 37,
            SGT = 38,
            SGE = 39,
            SLT = 40,
            SLE = 41
        }

        public BasicBlock BuildIcmp<T>(out ValueBool instruction, ComparePredicate predicate, IIntValue<T> lhs, IIntValue<T> rhs, string name) where T : struct, IIntValue {
            instruction = new ValueBool(InstructionLibrary.BuildIcmp(builder.builderPointer, (InstructionLibrary.ComparePredicate)predicate, lhs.valuePointer, rhs.valuePointer, name));
            return this;
        }
    }
}