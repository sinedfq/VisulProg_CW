﻿using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Converters
{
    public class SignalToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool signal && targetType.IsAssignableTo(typeof(IBrush)))
            {
                return signal ? new SolidColorBrush(Colors.LightGreen) : new SolidColorBrush(Colors.LightGray);
            }
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
