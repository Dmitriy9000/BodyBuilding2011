using System.Collections;
using System.IO;
using System.Xml.Serialization;

namespace BodyBuilding2011.Model
{
    [XmlInclude(typeof (Excercise))]
    public class Bookmarks
    {
        public ArrayList All;

        public Bookmarks()
        {
            All = new ArrayList();
        }

        public static Bookmarks LoadFromFile(string fn)
        {
            var xs = new XmlSerializer(typeof (Bookmarks));
            var fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
            var toReturn = (Bookmarks) xs.Deserialize(fs);
            fs.Close();
            return toReturn;
        }

        public void SaveToFile(string fn)
        {
            var xs = new XmlSerializer(typeof (Bookmarks));
            var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            xs.Serialize(fs, this);
            fs.Close();
        }
    }
}