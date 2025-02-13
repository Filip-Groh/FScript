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

            ValueInt32 constant = new ValueInt32(int32Type, -10);
            ValueInt32 constant2 = new ValueInt32(int32Type, 20);

            basicBlock.BuildAdd(out ValueInt32 added, constant, constant2, "added")
                .BuildAdd(out ValueInt32 added2, added, constant, "added2")
                .BuildSub(out ValueInt32 subbed, added2, constant2, "subbed")
                .BuildMul(out ValueInt32 mulled, subbed, constant, "mulled")
                .BuildRet(mulled);

            function.AppendBasicBlock(basicBlock);

            Console.WriteLine(module.PrintToString());
        }
    }
}
