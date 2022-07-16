using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Exam
{
    public class JsonWriter<T>: Writer<T> where T: class
    {
        private string _fileName;

        public JsonWriter(string fileName)
        {
            _fileName = fileName;
        }

        public override void Write(T data)
        {
            string jsonstr = JsonSerializer.Serialize<T>(data, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            });
            File.WriteAllText(_fileName, jsonstr);
        }
    }
}
