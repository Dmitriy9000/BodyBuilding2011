using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace BodyBuilding2011.Model
{
    public class Favorites
    {
        //public List<Excercise> ExcLib { get; set; }
        //public List<SuperSet> SetsLib { get; set; }

        public Favorites()
        {
            Lib = new ObservableCollection<string>();
            //ExcLib = new List<Excercise>();
            //SetsLib = new List<SuperSet>();
        }

        public ObservableCollection<string> Lib { get; set; }

        //public void AddToFavorites(string name)
        //{
        //    if (!Lib.Contains(name))
        //        Lib.Add(name);
        //}

        //public void RemoverFromFavorites(string name)
        //{
        //    if (Lib.Contains(name))
        //        Lib.Remove(name);
        //}

        public static Favorites LoadFromFile(string fn)
        {
            var xs = new XmlSerializer(typeof (Favorites));
            var fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
            var toReturn = (Favorites) xs.Deserialize(fs);
            fs.Close();
            return toReturn;
        }

        public void SaveToFile(string fn)
        {
            var xs = new XmlSerializer(typeof (Favorites));
            var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            xs.Serialize(fs, this);
            fs.Close();
        }
    }
}