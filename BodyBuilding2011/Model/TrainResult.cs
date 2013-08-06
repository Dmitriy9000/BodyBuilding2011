using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class TrainResult : INotifyPropertyChanged
    {
        [field:NonSerialized] 
        public TrainResults Parent;

        // Для сериализации
        public TrainResult(){}

        public TrainResult(DateTime dt, TrainResults owner)
        {
            Parent = owner;
            Uprs = new ObservableCollection<ExcerciseResult>();
            Uprs.CollectionChanged += Uprs_CollectionChanged;
            Date = dt;
        }

        public ObservableCollection<ExcerciseResult> Uprs { get; set; }
        public DateTime Date { get; set; }

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void DeserializationInit()
        {
            Uprs.CollectionChanged += Uprs_CollectionChanged;
        }

        private void Uprs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged(this, new PropertyChangedEventArgs("Uprs"));

            if (Uprs.Count == 0)
            {
                SelfDestruct();
            }

            //PropertyChanged(this, new PropertyChangedEventArgs("NotEmpty"));
        }

        private void SelfDestruct()
        {
            TrainResult toRem = Parent.TrainDays.Single(c => c.Date == Date);
            Parent.TrainDays.Remove(toRem);
        }

        public override string ToString()
        {
            string str;
            TimeSpan tm = DateTime.Now.Date - Date.Date;

            if (tm.Days < 0)
            {
                str = string.Empty;
            }
            else if (tm.Days == 0)
            {
                str = "(сегодня)";
            }
            else if (tm.Days == 1)
            {
                str = "(вчера)";
            }
            else if (tm.Days == 2)
            {
                str = "(позавчера)";
            }
            else
            {
                if (tm.Days > 4)
                {
                    str = "(" + tm.Days + " дней назад)";
                }
                else
                {
                    str = "(" + tm.Days + " дня назад)";
                }
            }
            return string.Format("{2} {0} {1}", Date.ToShortDateString(), str, Date.ToString("ddd"));
        }
    }
}