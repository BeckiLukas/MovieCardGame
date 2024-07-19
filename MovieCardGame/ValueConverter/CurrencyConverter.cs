using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MovieCardGame.ValueConverter
{
    class CurrencyConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var val = (decimal)value;
            if (val == -1)
            {
                return "N/A";
            }
            return val.ToString( "N0", culture ) + (string)parameter;

        }
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {

            throw new NotImplementedException();
        }
    }
}