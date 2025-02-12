using LLVMLibrary.BasicBlock;
using LLVMLibrary.Context;
using LLVMSharp.Interop;

namespace LLVMLibrary.Builder
{
    public unsafe struct BuilderPointer
    {
        public LLVMOpaqueBuilder* pointer;

        public BuilderPointer(LLVMOpaqueBuilder* pointer)
        {
            this.pointer = pointer;
        }
    }

    public static unsafe class BuilderLibrary
    {
        public static BuilderPointer CreateBuilder(ContextPointer contextPointer)
        {
            return new BuilderPointer(LLVM.CreateBuilderInContext(contextPointer.pointer));
        }

        public static void DisposeBuilder(BuilderPointer builderPointer)
        {
            LLVM.DisposeBuilder(builderPointer.pointer);
        }

        public static void PositionBuilderAtEnd(BuilderPointer builderPointer, BasicBlockPointer basicBlockPointer)
        {
            LLVM.PositionBuilderAtEnd(builderPointer.pointer, basicBlockPointer.pointer);
        }
    }
}
