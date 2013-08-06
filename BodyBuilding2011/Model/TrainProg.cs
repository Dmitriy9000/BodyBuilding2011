using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class TrainProg
    {
        private ObservableCollection<TrainProgDay> _cycle;
        public ObservableCollection<TrainProgDay> Cycle
        {
            get { return _cycle; }
            set { _cycle = value; }
        }

        public String Name { get; private set; }

        public TrainProg()
        {
            Cycle = new ObservableCollection<TrainProgDay>();
        }

        public TrainProg(ObservableCollection<TrainProgDay> cycle)
        {
            Cycle = cycle;
        }

        public TrainProgDay this[int i]
        {
            get { return Cycle[i%Cycle.Count]; }
        }

        public override string ToString()
        {
            //string res = "";
            //int i = 0;
            //foreach (MuscleGroupEnum day in Cycle)
            //{
            //    if (day.Count == 0)
            //        res += "День " + (i + 1) + " (отдых)\n";
            //    else
            //        res += "День " + (i + 1) + " (";
            //    foreach (MuscleGroup muscleGroup in day)
            //    {

            //        res += muscleGroup.Name + ", ";
            //    }
            //    res = res.Substring(0, res.Length - 2) + ")\n";
            //    i++;
            //}
            //return res;
            return "HUI";
        }

        public static TrainProg LoadFromFile(string fn)
        {
            var xs = new XmlSerializer(typeof (TrainProg));
            var fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
            var toReturn = (TrainProg) xs.Deserialize(fs);
            fs.Close();
            return toReturn;
        }

        public void SaveToFile(string fn)
        {
            var xs = new XmlSerializer(typeof (TrainProg));
            var fs = new FileStream(fn, FileMode.Create, FileAccess.Write);
            xs.Serialize(fs, this);
            fs.Close();
        }
    }
}