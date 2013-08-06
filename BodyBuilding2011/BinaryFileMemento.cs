using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Modeling
{
    [Serializable]
    internal class BinaryFileMemento<T>
    {
        private readonly T _data;

        public BinaryFileMemento(T graph)
        {
            _data = graph;
        }

        public void Save(string fn)
        {
            try
            {
                using (var fs = new FileStream(fn, FileMode.Create, FileAccess.Write))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(fs, _data);
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static T Load(string fn)
        {
            try
            {
                using (var fs = new FileStream(fn, FileMode.Open, FileAccess.Read))
                {
                    var bf = new BinaryFormatter();
                    object toRet = bf.Deserialize(fs);
                    fs.Close();
                    return (T) toRet;
                }
            }
            catch (Exception)
            {
                throw new SerializationException();
            }
        }
    }
}