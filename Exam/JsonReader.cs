using System.IO;
using System.Text.Json;

namespace Exam
{
    public class JsonReader<T>: Reader<T> where T: class
    {
        private string _fileName;

        public JsonReader(string fileName)
        {
            _fileName = fileName;
        }

        public override T Read()
        {
            if (File.Exists(_fileName))
            {
                string jsonstr = File.ReadAllText(_fileName);
                T data = JsonSerializer.Deserialize<T>(jsonstr);
                return data;
            }
            return null;
        }
    }
}
