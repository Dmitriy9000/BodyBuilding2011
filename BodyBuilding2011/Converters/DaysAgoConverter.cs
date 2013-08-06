using System;
using System.Globalization;
using System.Windows.Data;

namespace BodyBuilding2011.Converters
{
    internal class DaysAgoConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                return (DateTime.Now - (DateTime) value).Days;
            }
            return -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}