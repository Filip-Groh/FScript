namespace Lexer.Tokens {
    public interface ITyped<T> where T : Enum {
        public T type { get; set; }
    }
}
