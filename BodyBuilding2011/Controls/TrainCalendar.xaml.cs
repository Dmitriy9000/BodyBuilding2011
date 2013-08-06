using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BodyBuilding2011.Controls
{
    /// <summary>
    /// Interaction logic for TrainCalendar.xaml
    /// </summary>
    public partial class TrainCalendar : UserControl
    {
        #region Delegates

        public delegate void SelectedDateChangedDelagate();

        #endregion

        private ObservableCollection<DateTime> _markedDays;
        private DateTime _month;
        private TrainCalendarDay _selectedDay;

        public TrainCalendar()
        {
            InitializeComponent();

            SelectedDateChanged += TrainCalendar_SelectedDateChanged;
            MarkedDays = new ObservableCollection<DateTime>();
            MarkedDays.CollectionChanged += MarkedDays_CollectionChanged;

            Month = DateTime.Now;
            GenerateDays(Month);

            monthLb.Content = string.Format("{0:Y}", Month);
        }

        public ObservableCollection<DateTime> MarkedDays
        {
            get { return _markedDays; }
            set
            {
                _markedDays = value;
                FillMarkedDays();
            }
        }

        public TrainCalendarDay SelectedDay
        {
            get { return _selectedDay; }
            set
            {
                if (_selectedDay != null)
                {
                    _selectedDay.Selected = false;
                }
                value.Selected = true;
                _selectedDay = value;
                SelectedDateChanged();
            }
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

        public DateTime Month
        {
            get { return _month; }
            set
            {
                _month = value;
                GenerateDays(_month);
                monthLb.Content = string.Format("{0:Y}", Month);
            }
        }

        public event SelectedDateChangedDelagate SelectedDateChanged;

        private void MarkedDays_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FillMarkedDays();
        }

        private void FillMarkedDays()
        {
            if (MarkedDays.Count > 0)
            {
                foreach (TrainCalendarDay day in DaysPanel.Children)
                {
                    if (!MarkedDays.Contains(day.Date))
                    {
                        day.FirstCell.Background = Brushes.Transparent;
                    }
                    else
                    {
                        day.FirstCell.Background = Brushes.LightGreen;
                    }
                }
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
                var cur = (TrainCalendarDay) DaysPanel.Children[i];
                if (cur.Date.Date == dt.Date)
                {
                    return cur;
                }
            }
            return null;
        }

        private void TrainCalendar_SelectedDateChanged()
        {
        }

        public void GenerateDays(DateTime month)
        {
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            var start = new DateTime(month.Year, month.Month, 1);
            var end = new DateTime(month.Year, month.Month, daysInMonth);

            DaysPanel.Children.Clear();

            int prevMonthDays = ExtesionHelper.DayOfWeekNum(start.DayOfWeek) -
                                ExtesionHelper.DayOfWeekNum(DayOfWeek.Monday);
            for (int cur = ExtesionHelper.DayOfWeekNum(DayOfWeek.Monday);
                 cur < ExtesionHelper.DayOfWeekNum(start.DayOfWeek);
                 cur++)
            {
                DateTime date = start.PreviousMonth();
                date = new DateTime(date.Year, date.Month,
                                    DateTime.DaysInMonth(date.Year, date.Month) - prevMonthDays + cur);

                var cell = new TrainCalendarDay(this, date);
                DaysPanel.Children.Add(cell);
            }

            for (DateTime date = start; date < end.AddDays(1); date = date.AddDays(1))
            {
                var cell = new TrainCalendarDay(this, date);
                DaysPanel.Children.Add(cell);
            }

            var lastDayInEnd = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month));
            int cnt = 1;
            for (int cur = ExtesionHelper.DayOfWeekNum(lastDayInEnd.DayOfWeek);
                 cur < ExtesionHelper.DayOfWeekNum(DayOfWeek.Sunday);
                 cur++)
            {
                DateTime date = start.NextMonth();
                date = new DateTime(date.Year, date.Month, cnt);
                var cell = new TrainCalendarDay(this, date);
                DaysPanel.Children.Add(cell);
                cnt++;
            }

            if (DaysPanel.Children.Count == 35)
            {
                var lastDate = DaysPanel.Children[34] as TrainCalendarDay;
                for (int i = 0; i < 7; i++)
                {
                    DateTime date = lastDate.Date.AddDays(i);
                    var cell = new TrainCalendarDay(this, date);
                    DaysPanel.Children.Add(cell);
                }
            }
            else if (DaysPanel.Children.Count == 28)
            {
                var lastDate = DaysPanel.Children[27] as TrainCalendarDay;
                for (int i = 0; i < 14; i++)
                {
                    DateTime date = lastDate.Date.AddDays(i);
                    var cell = new TrainCalendarDay(this, date);
                    DaysPanel.Children.Add(cell);
                }
            }

            if (SelectedDay != null)
            {
                if ((SelectedDay.Date.Month == Month.Month) && (SelectedDay.Date.Year == Month.Year))
                {
                    DateTime date = SelectedDay.Date;
                    SelectedDay = GetDayInCurrentMonth(date);
                }
            }

            FillMarkedDays();
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

        private void DaysPanel_MouseUp(object sender, MouseButtonEventArgs e)
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