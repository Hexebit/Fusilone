using System;
using System.Globalization;
using System.Windows.Data;

namespace Fusilone.Converters;

public class DateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            if (date == DateTime.MinValue || date.Year < 2000)
                return "-";
            return date.ToString("dd.MM.yyyy");
        }
        return "-";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
