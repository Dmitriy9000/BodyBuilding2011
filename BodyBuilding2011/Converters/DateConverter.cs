using System;
using System.Globalization;
using System.Windows.Data;

namespace BodyBuilding2011.Converters
{
    internal class DateConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] s = string.Format("{0:M}", (DateTime) value).Split(' ');
            if (s[1][0] == '0')
                s[1] = s[1].Substring(1);
            return s[1] + " " + s[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}