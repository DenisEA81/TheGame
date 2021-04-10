using System;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;

namespace ToolLibrary
{
    public abstract class ASerializableXML<T>
    {
        public static void SaveToXML(string FileName, T[] collection, FileMode Mode = FileMode.Create)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T[]));
            using (FileStream fs = new FileStream(FileName, Mode))
                formatter.Serialize(fs, collection);
        }
        public static T[] LoadFromXML(string FileName)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T[]));
            using (FileStream fs = new FileStream(FileName, FileMode.Open))
                return (T[])formatter.Deserialize(fs);
        }
    }
}
