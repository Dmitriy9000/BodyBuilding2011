using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace BodyBuilding2011.Model
{
    [Serializable]
    public class ExcerciseResult : INotifyPropertyChanged
    {
        // для сериализации
        private ExcerciseResult(){}

        public ExcerciseResult(Excercise excercise, TrainResult p, int sets = 0)
        {
            Excercise = excercise;
            Sets = new ObservableCollection<Set>();
            Parent = p;
            if (excercise is SuperSet)
            {
                foreach (Excercise e in (excercise as SuperSet).ExcercisesList)
                {
                    Sets.Add(new Set(this, e.Name));
                }
            }
            else
            {
                for (int i = 0; i < sets; i++)
                {
                    Sets.Add(new Set(this, string.Format("{0}й подход", i + 1)));
                }
            }
            Sets.CollectionChanged += Sets_CollectionChanged;
            PropertyChanged += ExcerciseResult_PropertyChanged;
        }

        public Excercise Excercise { get; set; }
        public ObservableCollection<Set> Sets { get; set; }

        [field:NonSerialized]
        private TrainResult Parent;

        public string Name
        {
            get { return (Excercise == null) ? string.Empty : Excercise.Name; }
        }

        #region INotifyPropertyChanged Members

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void DeserializationInit()
        {
            Sets.CollectionChanged += Sets_CollectionChanged;
            PropertyChanged += ExcerciseResult_PropertyChanged;
        }

        private void ExcerciseResult_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        private void Sets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Sets.Count == 0)
            {
                Remove();
            }

            if (Excercise is Excercise)
            {
                RenameSetsByOrder();
            }
            PropertyChanged(this, new PropertyChangedEventArgs("Sets"));
        }

        private void RenameSetsByOrder()
        {
            for (int i = 0; i < Sets.Count; i++)
            {
                Sets[i].Name = string.Format("{0}й подход", i + 1);
            }
        }

        public void Remove()
        {
            Parent.Uprs.Remove(this);
        }
    }
}