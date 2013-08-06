using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace TrainCalendarTest
{
    /// <summary>
    /// Interaction logic for TrainCalendar.xaml
    /// </summary>
    public partial class TrainCalendar : UserControl
    {
        public delegate void SelectedDateChangedDelagate();
        public event SelectedDateChangedDelagate SelectedDateChanged;

        private TrainCalendarDay _selectedDay;
        private Brush _oldSelectedDayBackground;
        public TrainCalendarDay SelectedDay
        {
            get
            {
                return _selectedDay;
            }
            set
            {
                if (_selectedDay != null)
                {
                    _selectedDay.Selected = false;
                    _selectedDay.Background = _oldSelectedDayBackground;
                }
                    
                _selectedDay = value;
                _selectedDay.Selected = true;
                _oldSelectedDayBackground = _selectedDay.Background;
                _selectedDay.Background = (SolidColorBrush)Resources["ArtBlueBrush"];
                SelectedDateChanged();
            }
        }

        public void SetDate(DateTime dt)
        {
            if ((Month.Month == dt.Month) && (Month.Year == dt.Year))
            {
                // Тот же месяц
                SelectedDay = GetDayInCurrentMonth(dt);
            }
            else
            {
                Month = dt.Date;
                GenerateDays(Month);
                monthLb.Content = string.Format("{0:Y}", Month);
                SelectedDay = GetDayInCurrentMonth(dt);
            }
        }

        public TrainCalendarDay GetDayInCurrentMonth(DateTime dt)
        {
            for (int i = 0; i < DaysPanel.Children.Count; i++)
            {
                var cur = (TrainCalendarDay)DaysPanel.Children[i];
                if (cur.Date.Date == dt.Date)
                {
                    return cur;
                }
            }
            return null;
        }

        public DateTime? SelectedDate
        {
            get
            {
                if (SelectedDay == null)
                    return null;
                return SelectedDay.Date;
            }
        }

        private DateTime _month;
        public DateTime Month
        {
            get  { return _month; }
            set
            {
                _month = value;
                GenerateDays(_month);
                monthLb.Content = string.Format("{0:Y}", Month);
            }
        }

        public TrainCalendar()
        {
            SelectedDateChanged += new SelectedDateChangedDelagate(TrainCalendar_SelectedDateChanged);
            InitializeComponent();
            Month = DateTime.Now;
            GenerateDays(Month);
            monthLb.Content = string.Format("{0:Y}", Month);
        }

        void TrainCalendar_SelectedDateChanged()
        {
            
        }

        public void GenerateDays(DateTime month)
        {
            var daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            var start = new DateTime(month.Year, month.Month, 1);
            var end = new DateTime(month.Year, month.Month, daysInMonth);

            DaysPanel.Children.Clear();

            var prevMonthDays = ExtesionHelper.DayOfWeekNum(start.DayOfWeek) - ExtesionHelper.DayOfWeekNum(DayOfWeek.Monday);
            for (var cur = ExtesionHelper.DayOfWeekNum(System.DayOfWeek.Monday); cur < ExtesionHelper.DayOfWeekNum(start.DayOfWeek); cur++)
            {
                var date = start.PreviousMonth();
                date = new DateTime(date.Year, date.Month,
                                    DateTime.DaysInMonth(date.Year, date.Month) - prevMonthDays + cur);

                var cell = new TrainCalendarDay(this, date, Brushes.LightGray);
                DaysPanel.Children.Add(cell);
            }

            for (var d = start; d < end.AddDays(1); d = d.AddDays(1))
            {
                var cell = new TrainCalendarDay(this, d, Brushes.BlanchedAlmond);
                DaysPanel.Children.Add(cell);
            }

            var lastDayInEnd = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month));
            var cnt = 1;
            for (var cur = ExtesionHelper.DayOfWeekNum(lastDayInEnd.DayOfWeek); cur < ExtesionHelper.DayOfWeekNum(DayOfWeek.Sunday); cur++)
            {
                var date = start.NextMonth();
                date = new DateTime(date.Year, date.Month, cnt);
                var cell = new TrainCalendarDay(this, date, Brushes.LightGray);
                DaysPanel.Children.Add(cell);
                cnt++;
            }

            if (DaysPanel.Children.Count == 35)
            {
                var lastDate = DaysPanel.Children[34] as TrainCalendarDay;
                for (int i=0; i<7; i++)
                {
                    var cell = new TrainCalendarDay(this, lastDate.Date.AddDays(i), Brushes.LightGray);
                    DaysPanel.Children.Add(cell);
                }
            }
            else if (DaysPanel.Children.Count == 28)
            {
                var lastDate = DaysPanel.Children[27] as TrainCalendarDay;
                for (int i = 0; i < 14; i++)
                {
                    var cell = new TrainCalendarDay(this, lastDate.Date.AddDays(i), Brushes.LightGray);
                    DaysPanel.Children.Add(cell);
                }
            }

            if (SelectedDay != null)
            {
                if ((SelectedDay.Date.Month == Month.Month) && (SelectedDay.Date.Year == Month.Year))
                {
                    GetDayInCurrentMonth(SelectedDay.Date).Background = (SolidColorBrush)Resources["ArtBlueBrush"];
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Month = Month.PreviousMonth();
            GenerateDays(Month);
            monthLb.Content = string.Format("{0:Y}", Month);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Month = Month.NextMonth();
            GenerateDays(Month);
            monthLb.Content = string.Format("{0:Y}", Month);
        }

        private void DaysPanel_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }

    public static class ExtesionHelper
    {
        /// <summary>
        /// По умолчанию Вс - первый день, приходится переделывать что бы понедельник был первым днем недели
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int DayOfWeekNum(DayOfWeek d)
        {
            switch (d)
            {
                case DayOfWeek.Sunday:
                    return 7;
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                default:
                    throw new ArgumentOutOfRangeException("d");
            }
        }

        public static DateTime PreviousMonth(this DateTime dt)
        {
            if (dt.Month == 1)
            {
                dt = new DateTime(dt.Year - 1, 12, 1);
            }
            else
            {
                dt = dt.AddMonths(-1);
            }
            return dt;
        }

        public static DateTime NextMonth(this DateTime dt)
        {
            if (dt.Month == 11)
            {
                dt = new DateTime(dt.Year + 1, 1, 1);
            }
            else
            {
                dt = dt.AddMonths(1);
            }
            return dt;
        }
    }
}
