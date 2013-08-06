using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using BodyBuilding2011.Misc;

namespace BodyBuilding2011.Controls
{
    /// <summary>
    /// Interaction logic for TrainCalendarDay.xaml
    /// </summary>
    public partial class TrainCalendarDay : UserControl, INotifyPropertyChanged
    {
        private bool _selected;

        public TrainCalendarDay()
        {
            InitializeComponent();
        }

        public TrainCalendarDay(TrainCalendar calendar, DateTime date)
        {
            Calendar = calendar;
            Date = date;
            InitializeComponent();
        }

        public DateTime Date { get; set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
            }
        }

        public TrainCalendar Calendar { get; private set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Calendar.SelectedDay = this;
        }

        private void Control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BodyBuildingCommands.AddExcercise.Execute(null, null);
        }
    }
}