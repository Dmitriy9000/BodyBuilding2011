using System;
using System.Globalization;
using System.Windows.Data;

namespace BodyBuilding2011.Converters
{
    internal class CheckboxToFavoritesConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool) value;
            if (!v)
                return "Resources/star-empty.png";
            return "Resources/star.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}