using System;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class Excercise : ICloneable
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public MuscleGroup MuscleGroups { get; set; }

        //public string MuscleGroupsString
        //{
        //    get
        //    {
        //        var s = "";
        //        this.MuscleGroups.ToList().ForEach(c => s += c.Name + ", ");
        //        s = s.Substring(0, s.Length - 2);
        //        return s;
        //    }
        //}

        public override string ToString()
        {
            return Name;
        }

        #region ICloneable Members

        public object Clone()
        {
            var clone = new Excercise();
            clone.Info = Info;
            clone.Name = Name;
            clone.MuscleGroups = MuscleGroups;
            return clone;
        }

        #endregion

    }
}