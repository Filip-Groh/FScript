using LLVMLibrary.Module;

namespace LLVMManager
{
    public class Module : Disposable {
        public ModulePointer modulePointer { get; init; }
        public string name { get; init; }

        public Module(Context context, string name) {
            this.name = name;

            modulePointer = ModuleLibrary.CreateModule(context.contextPointer, name);
        }

        public string PrintToString() {
            return ModuleLibrary.PrintModuleToString(modulePointer);
        }

        public override void OnDispose() {
            ModuleLibrary.DisposeModule(modulePointer);
        }
    }
}
