using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace IWMB01_Flugsuche_App.Services;

public class FlightApiHelper
{
    private readonly FlightAccessHelper _flightAccessHelper;
    private readonly HttpClient _flightApiClient;
    private TokenResponseModel _token;

    public FlightApiHelper(IConfiguration configuration, FlightAccessHelper flightAccessHelper)
    {
        _flightAccessHelper = flightAccessHelper;

        var baseUri = configuration["LufthansaAPI:baseUri"];
        _flightApiClient = new HttpClient
        {
            BaseAddress = new Uri(baseUri)
        };
        _flightApiClient.DefaultRequestHeaders.Accept.Clear();
        _flightApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private async Task InitializeAsync()
    {
        _token = await _flightAccessHelper.GetAccessTokenAsync();
        _flightApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.access_token);
    }

    public async Task<T> GetFromJsonAsync<T>(string requestUri)
    {
        if (_token == null || _token.IsTokenExpired())
        {
            await InitializeAsync();
        }
        var response = await _flightApiClient.GetFromJsonAsync<T>(requestUri);
        return response;
    }
}