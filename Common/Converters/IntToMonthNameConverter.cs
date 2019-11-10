using Common.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Common.Converters
{
    public class IntToMonthNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateUtils.Months[12 - (int)value];
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 12 - DateUtils.Months.FindIndex(s => s == (string)value);
        }
    }
}
