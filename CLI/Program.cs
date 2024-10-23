using Cocona;
using Compiler;
using Lexer;
using Parser;

namespace CLI {
    internal class Program {
        public static readonly string VERSION = "0.0.1";

        static void Main(string[] args) {
            CoconaApp.Run((string? filename) => {
                if (filename is not null) {
                    ProcessFile(filename);
                }

                Console.WriteLine($"Running FScript interpreter with version {VERSION}");

                HandleInputs();
            });
        }

        static void ExecutePipeline(string code, bool runInterpreted) {
            Lexer.Lexer lexer = new Lexer.Lexer(code);
            Token[] tokens = lexer.Tokenize();

            Console.WriteLine(Token.ArrayToString(tokens));



            AST abstractSyntaxTree = Parser.Parser.Parse(tokens);



            Bytecode bytecode = Compiler.Compiler.CompileToBytecode(abstractSyntaxTree);

            if (runInterpreted) {
                Interpreter.Interpreter.Execute(bytecode);
            } else {
                JIT.JIT.Process(bytecode);
            }
        }

        static void ProcessFile(string filename) {
            FileReader fileReader = new FileReader(filename);
            string? content = fileReader.Read();

            if (content is null) {
                return;
            }

            ExecutePipeline(content, true);
        }

        static void HandleInputs() {
            while (true) {
                Console.Write("Command: ");
                string? input = Console.ReadLine();

                if (input is null || input == "") {
                    return;
                }

                ExecutePipeline(input, true);
            }
        }
    }
}
