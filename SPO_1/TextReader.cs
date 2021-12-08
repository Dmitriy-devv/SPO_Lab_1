using System.Text;
using System.IO;


namespace SPO_1
{
    public class TextReader
    {
        private string Path { get; set; }
        public TextReader(string path)
        {
            Path = path;
        }

        public string Read()
        {
            var textFromFile = string.Empty;
            using (FileStream fstream = File.OpenRead(Path))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                textFromFile = Encoding.Default.GetString(array);

            }
            return textFromFile;
        }
    }
}
