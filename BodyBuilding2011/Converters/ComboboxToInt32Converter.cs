using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace BodyBuilding2011.Converters
{
    internal class ComboboxToInt32Converter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = new ComboBoxItem();
            item.Content = value;
            return item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = (ComboBoxItem) value;
            string val = item.Content.ToString();
            return Int32.Parse(val);
        }

        #endregion
    }
}