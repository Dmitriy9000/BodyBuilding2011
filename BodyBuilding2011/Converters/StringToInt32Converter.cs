using System;
using System.Globalization;
using System.Windows.Data;

namespace BodyBuilding2011.Converters
{
    internal class StringToInt32Converter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int r;
            if (Int32.TryParse(value.ToString(), out r))
                return r;
            return 0;
        }

        #endregion
    }
}