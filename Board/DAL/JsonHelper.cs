using Board.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Board.DAL
{
    public class JsonHelper
    {
        string FilePath { get; } = String.Empty;

        public JsonHelper(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException(nameof(filePath), $"{nameof(filePath)} can not be null or empty");
            }

            this.FilePath = filePath;
        }

        public IEnumerable<Card> Deserialize()
        {
            using (StreamReader streamReader = new StreamReader(FilePath))
            {
                string json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Card>>(json);
            }
        }

        public async void Serialize(List<Card> cards)
        {
            await using (StreamWriter streamWriter = new StreamWriter(FilePath))
            using (JsonWriter writer = new JsonTextWriter(streamWriter))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, cards);
            }
        }
    }
}
