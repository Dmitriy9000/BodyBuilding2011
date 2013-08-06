using System;
using System.Globalization;
using System.Windows.Data;
using BodyBuilding2011.Model;
using BodyBuilding2011.Windows;

namespace BodyBuilding2011.Converters
{
    internal class TrainResultEmptyToDateConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TrainResult result = MainWindow.Results[(DateTime) parameter];
            if (result.Uprs.Count > 0)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}