using System;
using System.Globalization;
using System.Windows.Data;
using Fusilone.Helpers;

namespace Fusilone.Converters;

public class StatusDisplayConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var status = value?.ToString() ?? string.Empty;
        return SpecLocalization.GetStatusDisplayLabel(status);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString() ?? string.Empty;
    }
}
