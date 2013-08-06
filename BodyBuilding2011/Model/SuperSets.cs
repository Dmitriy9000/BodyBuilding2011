using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class SuperSets
    {
        public SuperSets()
        {
            Lib = new ObservableCollection<SuperSet>();
        }

        public ObservableCollection<SuperSet> Lib { get; set; }

        public SuperSet this[string name]
        {
            get { return Lib.Single(c => c.Name == name); }
        }

        public static SuperSets LoadFromFile(string fn)
        {
            var xs = new XmlSerializer(typeof (SuperSets));
            var fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
            var toReturn = (SuperSets) xs.Deserialize(fs);
            fs.Close();
            return toReturn;
        }

        public void SaveToFile(string fn)
        {
            var xs = new XmlSerializer(typeof (SuperSets));
            var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            xs.Serialize(fs, this);
            fs.Close();
        }
    }
}