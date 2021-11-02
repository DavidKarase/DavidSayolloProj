namespace Sayollo.Services
{
    public interface IXmlService
    {
        T XmlToObject<T>(string jsonString);
        string ObjectToXml<T>(T obj);
        T ReadXmlFile<T>(string filePath);
        void WriteXmlFile<T>(T obj, string filePath);
    }
}