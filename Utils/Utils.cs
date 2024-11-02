namespace Utils {
    public static class ArrayToString<T> {
        public static string ToString(T[] array, string separator = ", ") {
            string result = string.Empty;
            for (int i = 0; i < array.Length; i++) {
                if (i == 0) {
                    result += array[i];
                } else {
                    result += separator + array[i];
                }
            }

            return result;
        }
    }
}
