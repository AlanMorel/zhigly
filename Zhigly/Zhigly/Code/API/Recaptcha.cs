namespace Zhigly.Code.API
{
    public class Recaptcha
    {
        public const string SiteKey = "Enter Recaptcha Site Key Here";
        public const string Secret = "Enter Recaptcha Secret Here";

        private const string Url = "https://www.google.com/recaptcha/api/siteverify";
        
        public static string GetURL(string response)
        {
            string url = Url;
            url += "?secret=" + Secret;
            url += "&response=" + response;

            return url;
        }

        public string Success { get; set; }
    }
}