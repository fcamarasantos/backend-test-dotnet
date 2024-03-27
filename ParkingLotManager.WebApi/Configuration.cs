namespace ParkingLotManager.WebApi;

public static class Configuration
{
    public static string JwtKey = "IxN9fUjnX0OcZfUl3W44ew==!!!!====";

    //public static string ApiKeyName = "api_key";    
    public const string ApiKeyName = "api_key";    
    //public static string ApiKey = "parking_oPt4oylWx0X4wfnj";
    public const string ApiKey = "parking_oPt4oylWx0X4wfnj";
    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
