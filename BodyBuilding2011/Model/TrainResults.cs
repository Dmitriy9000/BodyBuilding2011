using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using BodyBuilding2011.Controls;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class TrainResults : INotifyPropertyChanged
    {
        [field: NonSerialized] private TrainCalendar _calendar;

        public TrainResults()
        {
            TrainDays = new ObservableCollection<TrainResult>();
            DeserializationInit();
        }

        public ObservableCollection<TrainResult> TrainDays { get; set; }

        public TrainResult this[DateTime date]
        {
            get
            {
                if (TrainDays != null)
                    if (TrainDays.Select(c => c.Date).Contains(date))
                        return TrainDays.Single(c => c.Date == date);
                return null;
            }
        }

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void SetCalendar(TrainCalendar calendar)
        {
            _calendar = calendar;
            _calendar.MarkedDays = new ObservableCollection<DateTime>(TrainDays.Select(c => c.Date));
        }

        public void DeserializationInit()
        {
            TrainDays.CollectionChanged += TrainDays_CollectionChanged;
            PropertyChanged += TrainResults_PropertyChanged;
            foreach (var trainResult in TrainDays)
            {
                trainResult.Parent = this;
            }
        }

        private void TrainResults_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        private void TrainDays_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _calendar.MarkedDays = new ObservableCollection<DateTime>(TrainDays.Select(c => c.Date));
        }
    }
}