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



            Parser.Parser parser = new Parser.Parser(tokens);
            AST abstractSyntaxTree = parser.Parse();

            Console.WriteLine(abstractSyntaxTree);



            Compiler.Compiler compiler = new Compiler.Compiler(abstractSyntaxTree);
            Bytecode[] bytecode = compiler.CompileToBytecode();

            Console.WriteLine(Compiler.Compiler.ArrayToString(bytecode));

            if (runInterpreted) {
                Interpreter.Interpreter interpreter = new Interpreter.Interpreter(bytecode);
                interpreter.Execute();

                Console.WriteLine(interpreter);
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
