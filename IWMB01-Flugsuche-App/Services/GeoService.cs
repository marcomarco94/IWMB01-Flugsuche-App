namespace IWMB01_Flugsuche_App.Services;

public class GeoService
{
    public async Task<Location> GetGeoCodeDataAsync(string address)
    {
        var locations = await Geocoding.GetLocationsAsync(address);
        Location location = locations?.FirstOrDefault();

        return location;
    }

    public async Task<Location> GetDeviceLocation()
    {
        Location location = await Geolocation.GetLastKnownLocationAsync();
        if (location == null)
        {
            location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(30)
            });
        }

        return location;
    }
}