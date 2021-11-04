using Newtonsoft.Json;
using System;
using System.IO;

namespace Sayollo.Services
{
    public class JsonService : IJsonService
    {
        private const string ERROR_FORMAT = "Sayollo.JsonService.{0}: {1}";

        public JsonService()
        {
            SingleManager.Register<IJsonService>(this);
        }

        public T ReadJsonFile<T>(string filePath)
        {
            try
            {
                StreamReader reader = new StreamReader(filePath);
                string json = reader.ReadToEnd();
                reader.Close();
                return JsonToObject<T>(json);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(ERROR_FORMAT, "ReadJsonFile", $"Failed to read file {filePath}.  {e.Message}"));
            }
        }

        public void WriteJsonFile<T>(T obj, string filePath, bool ignoreNulls = false)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                string json = ObjectToJson(obj, ignoreNulls, true);
                StreamWriter writer = new StreamWriter(filePath, true);
                writer.WriteLine(json);
                writer.Close();
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(ERROR_FORMAT, "WriteJsonFile", $"Failed to write file {filePath}.  {e.Message}"));
            }
        }

        public T JsonToObject<T>(string jsonString)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(ERROR_FORMAT, "JsonToObject", $"Couldn't convert JSON to object: {e.Message}.\n{jsonString}"));
            }
        }

        public string ObjectToJson<T>(T obj, bool ignoreNulls = false, bool indented = false)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = ignoreNulls ? NullValueHandling.Ignore : NullValueHandling.Include,
                Formatting = indented ? Formatting.Indented : Formatting.None,
            };
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
