using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MovieCardGame.ValueConverter
{
    class StringConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {

            if ( parameter != null && parameter.Equals( "Points" ) )
            {
                var num = (int)value;
                return "Points: " + num.ToString();
            }
            if ( value is System.Int32 )
            {
                var num = (int)value;
                return num.ToString( "N0", culture );
            }
         
            return value.ToString();
            
        }
        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {

            throw new NotImplementedException();  
        }
    }
}
