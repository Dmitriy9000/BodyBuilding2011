using System;
using System.Globalization;
using System.Windows.Data;
using BodyBuilding2011.Model;
using BodyBuilding2011.Windows;

namespace BodyBuilding2011.Converters
{
    internal class FavoritesToNameConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var excName = (Excercise) value;
            if (MainWindow.FavoritesLib.Lib.Contains(excName.Name))
                return true;
            //if (excName is Excercise)
            //{
            //    if (MainWindow.FavoritesLib.ExcLib.Select(c=>c.Name).Contains(excName.Name))
            //        return true;
            //}
            //else if (excName is SuperSet)
            //{
            //    if (MainWindow.FavoritesLib.SetsLib.Select(c=>c.Name).Contains(excName.Name))
            //        return true;
            //}
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}