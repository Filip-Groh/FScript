namespace CLI {
    internal class FileReader {
        public string path { get; init; } = string.Empty;

        public FileReader(string path) {
            this.path = path;
        }

        public string? Read() {
            try {
                string fileContent = string.Empty;

                using (StreamReader sr = new StreamReader(path)) {
                    string? line;

                    while ((line = sr.ReadLine()) != null) {
                        fileContent += line;
                    }
                }

                return fileContent;
            } catch (Exception) {
                return null;
            }
        }
    }
}
