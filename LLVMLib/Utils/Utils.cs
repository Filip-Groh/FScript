using LLVMSharp.Interop;

namespace LLVMLibrary
{
    public static class Utils
    {
        public static unsafe sbyte* StringToSByte(string text)
        {
            MarshaledString str = new MarshaledString(text.AsSpan());
            try
            {
                return str.Value;
            }
            finally
            {
                ((IDisposable)str).Dispose();
            }
        }

        public static unsafe string SByteToString(sbyte* ptr)
        {
            if (ptr == null)
            {
                return string.Empty;
            }

            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(ptr, int.MaxValue);
            string result = span.Slice(0, span.IndexOf<byte>(0)).AsString();

            return result;
        }
    }
}
