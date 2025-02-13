﻿using LLVMLibrary.Builder;
using LLVMLibrary.Value.Interface;
using LLVMSharp.Interop;

namespace LLVMLibrary.Instruction {
    public static unsafe partial class InstructionLibrary {
        public static T BuildOr<T>(BuilderPointer builderPointer, T lhs, T rhs, string name) where T : IIntValue, new() {
            return new T() { pointer = LLVM.BuildOr(builderPointer.pointer, lhs.pointer, rhs.pointer, Utils.StringToSByte(name)) };
        }
    }
}