using System.Collections.Generic;

namespace BodyBuilding2011.Model
{
    public class TrainProgDay
    {
        private readonly TrainProg _prog;
        
        public TrainProgDay(){}

        public TrainProgDay(TrainProg prog)
        {
            _prog = prog;
            Groups = new List<MuscleGroup>();
        }

        public List<MuscleGroup> Groups { get; set; }

        public override string ToString()
        {
            var index = _prog.Cycle.IndexOf(this);
            return string.Format("День {0}", index + 1);
        }

    }
}