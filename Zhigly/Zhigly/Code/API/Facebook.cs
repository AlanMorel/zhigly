using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Zhigly.Code.API
{
    public class Facebook
    {
        public static int Likes = 0;

        private const string Username = "zhigly";
        private const string AccessToken = "Enter Access Token Here";
        private const string Secret = "Enter Secret Here";

        private const string Url = "https://graph.facebook.com/";

        public static int GetLikes()
        {
            string url = Url + Username;
            url += "?access_token=" + AccessToken;
            url += "|" + Secret;
            url += "&fields=likes";

            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    JObject page = JObject.Parse(json);
                    JToken likes = page["likes"];

                    return (int)likes;
                }
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }

            return 0;
        }
    }
}