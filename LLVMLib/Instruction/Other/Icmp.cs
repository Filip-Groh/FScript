using LLVMLibrary.Builder;
using LLVMLibrary.Value;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Instruction {
    public static unsafe partial class InstructionLibrary {
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

        public static ValueBoolPointer BuildIcmp<T>(BuilderPointer builderPointer, ComparePredicate predicate, T lhs, T rhs, string name) where T : IIntValue, new() {
            return new ValueBoolPointer(LLVM.BuildICmp(builderPointer.pointer, (LLVMIntPredicate)predicate, lhs.pointer, rhs.pointer, Utils.StringToSByte(name)));
        }
    }
}