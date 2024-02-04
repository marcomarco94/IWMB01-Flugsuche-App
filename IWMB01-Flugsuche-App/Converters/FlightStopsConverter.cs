using System.Collections;

namespace IWMB01_Flugsuche_App.Converters;

public class FlightStopsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var flights = value as List<Flight>;
        if (flights == null || flights.Count == 0)
            return null;

        return flights.Count - 1;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}