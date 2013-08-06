using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class TrainDay
    {
        public MuscleGroup MuscleGroups;
        public DateTime Date { get; set; }
    }

    [Serializable]
    public class TrainDiary
    {
        public List<TrainDay> Days;

        public TrainDiary()
        {
            Days = new List<TrainDay>();
        }

        //public static TrainDiary FromAppointments(AppointmentStorage storage)
        //{
        //    TrainDiary diary = new TrainDiary();

        //    List<Appointment> buf = new List<Appointment>();
        //    for (int i = 0; i < storage.Count; i++)
        //    {
        //        buf.Add(storage[i]);

        //    }

        //    List<DateTime> notEmptyDays = buf.Select(c => c.Start.Date).Distinct().ToList();

        //    foreach (DateTime day in notEmptyDays)
        //    {
        //        DateTime time = day;
        //        List<string> s = buf.Where(c => c.Start.Date == time.Date).Select(c => c.Subject).ToList();
        //        diary.Days.Add(new TrainDay
        //                           {
        //                               Date = day + TimeSpan.FromSeconds(1),
        //                               MuscleGroups = s.Select(c => MuscleGroups.GetGroup(c)).ToList(),
        //                               //Result = new TrainResult(),
        //                           });
        //    }
        //    return diary;
        //}

        public static TrainDiary InitializeDiary(TrainProg prog, DateTime startdate, DateTime endDate)
        {
            var assemblingDiary = new TrainDiary {Days = new List<TrainDay>()};
            DateTime d = startdate;
            TimeSpan period = endDate - startdate;

            for (int i = 0; i < period.Days; i++)
            {
                var cur = new TrainDay
                              {
                                  Date = d,
                                  //MuscleGroups = programm[i],
                                  //Result = new TrainResult(),
                              };

                assemblingDiary.Days.Add(cur);
                d += TimeSpan.FromDays(1);
            }

            return assemblingDiary;
        }

        public static TrainDiary LoadFromFile(string fn)
        {
            var bf = new BinaryFormatter();
            var fs = new FileStream(fn, FileMode.Open, FileAccess.Read);
            var toReturn = (TrainDiary) bf.Deserialize(fs);
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