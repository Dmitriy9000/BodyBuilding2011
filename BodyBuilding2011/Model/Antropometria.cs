using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class AntropometricalParameters
    {
        public List<string> All;

        public AntropometricalParameters()
        {
            All = new List<string>
                      {
                          "Вес",
                          "Шея",
                          "Грудь",
                          "Бицепс",
                          "Предплечье",
                          "Запястье",
                          "Талия",
                          "Бедро",
                          "Икра"
                      };
        }

        public static AntropometricalParameters LoadFromFile(string fn)
        {
            var bf = new BinaryFormatter();
            var fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
            var toReturn = (AntropometricalParameters) bf.Deserialize(fs);
            fs.Close();
            return toReturn;
        }

        public void SaveToFile(string fn)
        {
            var bf = new BinaryFormatter();
            var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            bf.Serialize(fs, this);
            fs.Close();
        }
    } ;

    [Serializable]
    internal class AntropometriaMetering
    {
        public DateTime Date;
        public string Parameter;
        public float Value;

        public AntropometriaMetering(DateTime d, string p, float v)
        {
            Date = d;
            Parameter = p;
            Value = v;
        }

        public override string ToString()
        {
            return Date.ToShortDateString() + " - " + Value;
        }
    }

    [Serializable]
    internal class AntropometriaHistory
    {
        public List<AntropometriaMetering> History;

        public AntropometriaHistory()
        {
            History = new List<AntropometriaMetering>();
        }

        public static AntropometriaHistory LoadFromFile(string fn)
        {
            var bf = new BinaryFormatter();
            var fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
            var toReturn = (AntropometriaHistory) bf.Deserialize(fs);
            fs.Close();
            return toReturn;
        }

        public void SaveToFile(string fn)
        {
            var bf = new BinaryFormatter();
            var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            bf.Serialize(fs, this);
            fs.Close();
        }
    }
}