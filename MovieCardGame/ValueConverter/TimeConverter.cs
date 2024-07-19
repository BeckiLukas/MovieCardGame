using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MovieCardGame.ValueConverter
{
    class TimeConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( (string)parameter == "min" )
            {
                var val = (int)value;
                return val.ToString() + (string)parameter;
            }
            return value.ToString();

        }
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {

            throw new NotImplementedException();
        }
    }
}
