using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Zhigly.Code.API
{
    public class Twitter
    {
        public static int Followers = 0;

        private const string Token = "Enter Twitter Token Here";
        private const string TokenSecret = "Enter Twitter Token Secret Here";
        private const string ConsumerKey = "Enter Consumer Key Here";
        private const string ConsumerKeySecret = "Enter Consumer Key Secret Here";

        private const string Version = "1.0";
        private const string SignatureMethod = "HMAC-SHA1";

        private const string GetUsersShow = "https://api.twitter.com/1.1/users/show.json";

        public static int GetTwitterFollowers()
        {
            string result = TwitterGet(
                GetUsersShow,
                new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("screen_name", "zhigly")
                });

            JObject data = JObject.Parse(result);
            JToken followers = data["followers_count"];

            return (int)followers;
        }

        private static string TwitterGet(string resourceUrl, IEnumerable<KeyValuePair<string, string>> RequestParameters)
        {
            return TwitterCall("GET", resourceUrl, RequestParameters);
        }

        private static string TwitterPost(string resourceUrl, IEnumerable<KeyValuePair<string, string>> RequestParameters)
        {
            return TwitterCall("POST", resourceUrl, RequestParameters);
        }

        private static string TwitterCall(string method, string resourceUrl, IEnumerable<KeyValuePair<string, string>> RequestParameters)
        {
            var oauthNonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauthTimestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            var authorizationParameters = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("oauth_consumer_key", ConsumerKey),
                new KeyValuePair<string, string>("oauth_nonce", oauthNonce),
                new KeyValuePair<string, string>("oauth_signature_method", SignatureMethod),
                new KeyValuePair<string, string>("oauth_timestamp", oauthTimestamp),
                new KeyValuePair<string, string>("oauth_token", Token),
                new KeyValuePair<string, string>("oauth_version", Version)
            };

            var allParameters = authorizationParameters.Union(RequestParameters).OrderBy(tmp => tmp.Key);

            var baseString = string.Join("&", allParameters.Select(p => string.Format("{0}={1}", p.Key, Uri.EscapeDataString(p.Value))));
            baseString = string.Format("{0}&{1}&{2}", method, Uri.EscapeDataString(resourceUrl), Uri.EscapeDataString(baseString));

            var compositeKey = string.Format("{0}&{1}", Uri.EscapeDataString(ConsumerKeySecret), Uri.EscapeDataString(TokenSecret));

            string oauthSignature = null;

            using (HMACSHA1 hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oauthSignature = Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString)));
            }

            var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                               "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                               "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(
                headerFormat,
                Uri.EscapeDataString(oauthNonce),
                Uri.EscapeDataString(SignatureMethod),
                Uri.EscapeDataString(oauthTimestamp),
                Uri.EscapeDataString(ConsumerKey),
                Uri.EscapeDataString(Token),
                Uri.EscapeDataString(oauthSignature),
                Uri.EscapeDataString(Version)
            );

            ServicePointManager.Expect100Continue = false;

            var parameterString = string.Join("&", RequestParameters.Select(p => string.Format("{0}={1}", p.Key, Uri.EscapeDataString(p.Value))));
            resourceUrl += "?" + parameterString;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resourceUrl);

            request.Headers.Add("Authorization", authHeader);
            request.Method = method;
            request.ContentType = "application/x-www-form-urlencoded";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}