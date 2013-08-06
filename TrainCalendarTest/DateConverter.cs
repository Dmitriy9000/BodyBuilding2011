using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace TrainCalendarTest
{
    class DateConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = string.Format("{0:M}", (DateTime) value).Split(' ');
            if (s[1][0] == '0')
                s[1] = s[1].Substring(1);
            return s[1] + " " + s[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
