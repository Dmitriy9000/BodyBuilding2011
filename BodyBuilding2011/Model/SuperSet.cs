using System;
using System.Collections.Generic;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class SuperSet : Excercise
    {
        public List<Excercise> ExcercisesList;

        public SuperSet()
        {
            ExcercisesList = new List<Excercise>();
        }

        public new MuscleGroup MuscleGroups
        {
            get
            {
                MuscleGroup group = 0;
                foreach (Excercise excercise in ExcercisesList)
                {
                    group = group | excercise.MuscleGroups;
                }
                return group;
            }
            set{}
        }

        public override string ToString()
        {
            string s = "Упражнения суперсета:\n";
            foreach (Excercise excercise in ExcercisesList)
            {
                s += " - " + excercise.Name + "\n";
            }
            return s;
        }
    }
}