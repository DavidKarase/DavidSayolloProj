namespace Sayollo.Services
{
    public interface IJsonService
    {
        T JsonToObject<T>(string jsonString);
        string ObjectToJson<T>(T obj, bool ignoreNulls = false, bool indented = false);
        T ReadJsonFile<T>(string filePath);
        void WriteJsonFile<T>(T obj, string filePath, bool ignoreNulls = false);
    }
}