namespace IWMB01_Flugsuche_App.Services;

public class FlightService
{
    private readonly FlightApiHelper _flightApiHelper;
    private readonly GeoService _geoService;

    public FlightService(FlightApiHelper flightApiHelper, GeoService geoService)
    {
        _flightApiHelper = flightApiHelper;
        _geoService = geoService;
    }
    
    private async Task<FlightResponseModel> GetClosestAirportAsync(Location location)
    {
        string adjustedLatitude = Math.Round(location.Latitude, 3).ToString(CultureInfo.InvariantCulture);
        string adjustedLongitude = Math.Round(location.Longitude, 3).ToString(CultureInfo.InvariantCulture);
        
       var response = await _flightApiHelper.GetFromJsonAsync<FlightResponseModel>($"mds-references/airports/nearest/{adjustedLatitude},{adjustedLongitude}?lang=DE");
       return response;
    }
    
    public async Task<FlightResponseModel> GetAirportByCodeAsync(string code)
    {
        var response = await _flightApiHelper.GetFromJsonAsync<FlightResponseModel>($"mds-references/airports/{code}?lang=DE&offset=0&LHoperated=0&group=AllAirports");
        return response;
    }

    public async Task<FlightResponseModel> GetAirportByCurrentLocationAsync()
    {
        Location location = await _geoService.GetDeviceLocation();
        var airports = await GetClosestAirportAsync(location);
        return airports;
    }

    public async Task<FlightResponseModel> GetAirportByCityAsync(string city)
    {
        Location location = await _geoService.GetGeoCodeDataAsync(city);
        var airport = await GetClosestAirportAsync(location);
        return airport;
    }
    
    public async Task<FlightResponseModel> GetScheduledFlightsAsync(string startAirportCode, string targetAirportCode, DateTime selectedDate)
    {
        var flights = await _flightApiHelper.GetFromJsonAsync<FlightResponseModel>($"operations/schedules/{startAirportCode}/{targetAirportCode}/{selectedDate:yyyy-MM-dd}?directFlights=0");
        return flights;
    }
}