using System.Globalization;
using System.Windows.Data;

namespace BankSystem.Models
{
    public class FloatToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float floatValue)
            {
                return floatValue.ToString();
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is string stringValue)
            {
                if (float.TryParse(stringValue, out float floatValue))
                {
                    return floatValue;
                }
            }

            return value;
        }
    }
}
