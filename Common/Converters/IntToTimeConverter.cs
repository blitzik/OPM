﻿using Common.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Common.Converters
{
    public class IntToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (new Time((int)value).HoursAndMinutes);
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((new Time((string)value)).TotalSeconds);
        }
    }
}
