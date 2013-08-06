using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using BodyBuilding2011.Controls;

namespace BodyBuilding2011.Converters
{
    internal class CalendarDayPositionToBrushConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var calendarDate = (TrainCalendarDay) value;

            if (calendarDate.Date.Date == DateTime.Now.Date)
                return Brushes.SkyBlue;
            else
            {
                if (calendarDate.Calendar != null)
                {
                    if ((calendarDate.Date.Month == calendarDate.Calendar.Month.Month) &&
                        (calendarDate.Date.Year == calendarDate.Calendar.Month.Year))
                        return Brushes.BlanchedAlmond;
                    else
                    {
                        return Brushes.LightGray;
                    }
                }
                else
                {
                    return Brushes.Red;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}