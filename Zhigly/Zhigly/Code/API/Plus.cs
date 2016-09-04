using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Zhigly.Code.API
{
    public class Plus
    {
        public static int Followers = 0;

        private const string Id = "Enter Google Plus ID Here";
        private const string Key = "Enter Google Plus Key Here";

        private const string Url = "https://www.googleapis.com/plus/v1/people/";

        public static int GetFollowers()
        {
            string url = Url + Id;
            url += "?key=" + Key;

            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);

                    JObject page = JObject.Parse(json);
                    JToken followers = page["plusOneCount"];

                    return (int)followers;
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