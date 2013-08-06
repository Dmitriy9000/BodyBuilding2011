using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using BodyBuilding2011.Model;

namespace BodyBuilding2011.Converters
{
    internal class TwoListsToIExcerciseListConverters : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var exc = (List<Excercise>) values[0];
            var sets = (List<SuperSet>) values[1];
            var nw = new List<Excercise>();
            exc.ToList().ForEach(nw.Add);
            sets.ToList().ForEach(nw.Add);
            return nw;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}