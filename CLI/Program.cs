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

            Console.WriteLine("\n---   Tokens   ---");
            Console.WriteLine(Utils.ArrayToString<Token>.ToString(tokens));

            Parser.Parser parser = new Parser.Parser(tokens);
            AST abstractSyntaxTree = parser.Parse();

            Console.WriteLine("\n---   Abstract Syntax Tree   ---");
            Console.WriteLine(abstractSyntaxTree);

            Compiler.Compiler compiler = new Compiler.Compiler(abstractSyntaxTree);
            Bytecode[] bytecode = compiler.CompileToBytecode();

            Console.WriteLine("\n---   Bytecode   ---");
            Console.WriteLine(Utils.ArrayToString<Bytecode>.ToString(bytecode, "\n"));

            if (runInterpreted) {
                Interpreter.Interpreter interpreter = new Interpreter.Interpreter(bytecode);
                interpreter.Execute();

                Console.WriteLine("\n---   Runtime   ---");
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
                Console.WriteLine();
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
