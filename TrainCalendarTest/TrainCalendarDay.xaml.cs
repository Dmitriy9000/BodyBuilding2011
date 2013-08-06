using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrainCalendarTest
{
    /// <summary>
    /// Interaction logic for TrainCalendarDay.xaml
    /// </summary>
    public partial class TrainCalendarDay : UserControl, INotifyPropertyChanged
    {
        private DateTime _date;
        public DateTime Date
        {

            get { return _date; }
            set
            {
                _date = value;
                
            }
        }

        public TrainResult Results { get; private set; }

        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
            }
        }

        private readonly TrainCalendar _calendar;

        public TrainCalendarDay()
        {
            InitializeComponent();
        }

        public TrainCalendarDay(TrainCalendar calendar, DateTime date, Brush bg)
        {
            InitializeComponent();
            _date = date;
            _calendar = calendar;

            if (_date.Date == DateTime.Now.Date)
                Background = Brushes.LightYellow;
            else
                Background = bg;
            
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _calendar.SelectedDay = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
