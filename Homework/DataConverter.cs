using System.Globalization;
using System.Windows.Data;
using Utilites;

namespace ClientWFP
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long timestump)
            {
                DateTime dateTime = Utils.UnixTimeStampToDateTime(timestump);
                return dateTime.ToString("dd.MM.yyyy");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
