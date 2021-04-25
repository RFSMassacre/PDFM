using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace The_Please_Dont_Fail_Me_Simulator.Data
{
    public class BinaryData
    {
        public static void Write<T>(string path, T objectToWrite)
        {
            FileStream stream = File.Open(path, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
            stream.Close();
        }

        public static T Read<T>(string path)
        {
            FileStream stream = File.Open(path, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            if (stream.Length == 0)
            {
                return default;
            }
            else
            {
                T t = (T)binaryFormatter.Deserialize(stream);
                stream.Close();
                return t;
            }
        }
    }
}
