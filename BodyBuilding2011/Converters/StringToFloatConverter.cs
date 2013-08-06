using System;
using System.Globalization;
using System.Windows.Data;

namespace BodyBuilding2011.Converters
{
    internal class StringToFloatConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float f;
            if (float.TryParse(value.ToString(), out f))
                return f;
            return 0;
        }

        #endregion
    }
}