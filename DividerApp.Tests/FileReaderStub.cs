namespace DividerApp.Tests
{
    /// <summary>
    /// Тестовый двойник, имитирующий чтение текста из файла.
    /// Возвращает заранее подготовленную строку вместо реального чтения.
    /// </summary>
    public class FileReaderStub
    {
        private readonly string _content;

        public FileReaderStub(string content)
        {
            _content = content;
        }

        public string ReadAllText(string filePath)
        {
            return _content;
        }
    }
}