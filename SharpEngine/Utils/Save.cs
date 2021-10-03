using System.Collections.Generic;
using System.IO;

namespace SharpEngine
{
    public class Save
    {
        private Dictionary<string, object> data;

        public Save(Dictionary<string, object> defaut)
        {
            data = defaut;
        }

        public object GetObject(string key)
        {
            return data.GetValueOrDefault(key, null);
        }

        public T GetObjectAs<T>(string key)
        {
            return data.ContainsKey(key) ? (T) data[key] : default;
        }

        public void SetObject(string key, object value)
        {
            data[key] = value;
        }

        public void Write(string filename)
        {
            using (BinaryWriter bW = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                foreach (KeyValuePair<string, object> dt in data)
                {
                    bW.Write(dt.Key);
                    if (dt.Value is bool v)
                    {
                        bW.Write("b");
                        bW.Write(v);
                    }
                    else if (dt.Value is int v1)
                    {
                        bW.Write("i");
                        bW.Write(v1);
                    }
                    else if (dt.Value is double v3)
                    {
                        bW.Write("d");
                        bW.Write(v3);
                    }
                    else
                    {
                        bW.Write("s");
                        bW.Write(dt.Value.ToString());
                    }
                }
            }
        }

        public static Save Load(string fileName, Dictionary<string, object> defaut)
        {
            Save temp = new Save(defaut);
            temp.Load(fileName);
            return temp;
        }

        public void Load(string fileName) 
        {
            if (File.Exists(fileName))
            {
                data.Clear();
                using (BinaryReader bR = new BinaryReader(File.OpenRead(fileName)))
                {
                    try
                    {
                        while (bR.ReadString() is string key)
                        {
                            string type = bR.ReadString();
                            object value;
                            if (type == "b")
                                value = bR.ReadBoolean();
                            else if (type == "i")
                                value = bR.ReadInt32();
                            else if (type == "d")
                                value = bR.ReadDouble();
                            else
                                value = bR.ReadString();
                            data.Add(key, value);
                        }
                    }
                    catch(EndOfStreamException)
                    { }
                }
            }
        }
    }
}
