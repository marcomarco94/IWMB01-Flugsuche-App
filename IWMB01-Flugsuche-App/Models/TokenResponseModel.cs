namespace IWMB01_Flugsuche_App.Models;

public class TokenResponseModel
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public DateTime expiration_time { get; set; }

    public bool IsTokenExpired()
    {
        return DateTime.Now >= expiration_time;
    }
}