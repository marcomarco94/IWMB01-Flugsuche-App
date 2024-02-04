using System.Net.Http.Json;

namespace IWMB01_Flugsuche_App.Services;

public class FlightAccessHelper
{
    private readonly IConfiguration _configuration;
    
    public FlightAccessHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<TokenResponseModel> GetAccessTokenAsync()
    {
        string clientId = _configuration["LufthansaAPI:clientId"];
        string clientSecret = _configuration["LufthansaAPI:clientSecret"];
        string authority = "https://api.lufthansa.com/v1/oauth/token";

        using (var client = new HttpClient())
        {
            var credentials = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "grant_type", "client_credentials" }
            };

            var response = await client.PostAsync(authority, new FormUrlEncodedContent(credentials));

            if (response.IsSuccessStatusCode)
            {
                TokenResponseModel tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponseModel>();
                tokenResponse.expiration_time = DateTime.Now.AddSeconds(tokenResponse.expires_in);
                return tokenResponse;
            }
            else
            {
                throw new Exception("Failed to get access token");
            }
        }
    }
}