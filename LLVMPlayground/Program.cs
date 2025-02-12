using LLVMManager;
using LLVMManager.Types;
using LLVMManager.Value;
using LLVMManager.Value.Const;

namespace LLVMPlayground
{
    public class Program {
        public static void Main(string[] args) {
            Context context = new Context();
            Builder builder = new Builder(context);
            Module module = new Module(context, "my_abstracted_module");

            Int32Type int32Type = new Int32Type(context);

            FunctionType functionType = new FunctionType(int32Type, []);

            Function function = new Function(context, module, "my_abstracted_function", functionType);

            BasicBlock basicBlock = new BasicBlock(context, "my_abstracted_basic_block");

            function.AppendBasicBlock(basicBlock);

            builder.SetPosition(basicBlock);

            ConstInt32 constant = new ConstInt32(int32Type, 10, 0);
            ConstInt32 constant2 = new ConstInt32(int32Type, 20, 0);

            ValueInt32 added = builder.BuildAdd(constant, constant2, "added");
            ValueInt32 added2 = builder.BuildAdd(added, (ValueInt32)constant, "added2");

            builder.BuildRet(added2);

            Console.WriteLine(module.PrintToString());
        }
    }
}
