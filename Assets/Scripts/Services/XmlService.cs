using System.Xml.Serialization;
using System;
using System.IO;
using System.Text;

namespace Sayollo.Services
{
    public class XmlService : IXmlService
    {
        private const string ERROR_FORMAT = "Sayollo.XmlService.{0}: {1}";

        public XmlService()
        {
            SingleManager.Register<IXmlService>(this);
        }

        public T ReadXmlFile<T>(string filePath)
        {
            try
            {
                StreamReader reader = new StreamReader(filePath);
                string xml = reader.ReadToEnd();
                reader.Close();
                return XmlToObject<T>(xml);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(ERROR_FORMAT, "ReadXmlFile", $"Failed to read file {filePath}.  {e.Message}"));
            }
        }

        public void WriteXmlFile<T>(T obj, string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                string xml = ObjectToXml(obj);
                StreamWriter writer = new StreamWriter(filePath, true);
                writer.WriteLine(xml);
                writer.Close();
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(ERROR_FORMAT, "WriteXmlFile", $"Failed to write file {filePath}.  {e.Message}"));
            }
        }

        public T XmlToObject<T>(string xmlString)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                T result;

                using (TextReader reader = new StringReader(xmlString))
                {
                    result = (T)serializer.Deserialize(reader);
                }

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(ERROR_FORMAT, "XmlToObject", $"Couldn't convert xml to object: {e.Message}.\n{xmlString}"));
            }
        }

        public string ObjectToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, obj);
            }

            return sb.ToString();
        }
    }
}