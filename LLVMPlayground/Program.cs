using LLVMManager;
using LLVMManager.Types;
using LLVMManager.Value;

namespace LLVMPlayground {
    public class Program {
        public static void Main(string[] args) {
            Context context = new Context();
            Module module = new Module(context, "my_abstracted_module");

            Int32Type int32Type = new Int32Type(context);

            FunctionType functionType = new FunctionType(int32Type, []);

            Function function = new Function(context, module, "my_abstracted_function", functionType);

            BasicBlock basicBlock = new BasicBlock(context, "my_abstracted_basic_block");

            ValueInt32 constantMinus10 = new ValueInt32(int32Type, -10);
            ValueInt32 constant20 = new ValueInt32(int32Type, 20);
            ValueInt32 constant0 = new ValueInt32(int32Type, 0);
            ValueInt32 constant1 = new ValueInt32(int32Type, 1);
            ValueInt32 constant2 = new ValueInt32(int32Type, 2);
            ValueInt32 constant3 = new ValueInt32(int32Type, 3);

            //basicBlock.BuildAdd(out ValueInt32 added, constantMinus10, constant20, "added")
            //    .BuildAdd(out ValueInt32 added2, added, constantMinus10, "added2")
            //    .BuildSub(out ValueInt32 subbed, added2, constant20, "subbed")
            //    .BuildMul(out ValueInt32 mulled, subbed, constantMinus10, "mulled")
            //    .BuildRet(mulled);

            //basicBlock.BuildAnd(out ValueInt32 anded, constant1, constant3, "anded")
            //    .BuildRet(anded);

            //basicBlock.BuildOr(out ValueInt32 ored, constant1, constant2, "ored")
            //    .BuildRet(ored);

            //basicBlock.BuildXor(out ValueInt32 xored, constant1, constant3, "xored")
            //    .BuildRet(xored);

            //basicBlock.BuildShl(out ValueInt32 shifedLeft, constant3, constant2, "shifedLeft")
            //    .BuildRet(shifedLeft);

            //basicBlock.BuildAshr(out ValueInt32 shifedRight, constant3, constant1, "shifedRight")
            //    .BuildRet(shifedRight);

            basicBlock.BuildIcmp(out ValueBool compared, BasicBlock.ComparePredicate.EQ, constant1, constant1, "compared")
                .BuildRet(compared);

            function.AppendBasicBlock(basicBlock);

            Console.WriteLine(module.PrintToString());
        }
    }
}
