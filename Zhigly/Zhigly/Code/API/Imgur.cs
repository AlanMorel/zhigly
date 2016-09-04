using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace Zhigly.Code.API
{
    public class Imgur
    {
        private const string ApiUrl = "https://api.imgur.com/3/upload.xml";
        private const string ClientId = "Enter Client ID Here";

        public static string GetUrl(byte[] image)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    var values = new NameValueCollection { { "image", Convert.ToBase64String(image) } };
                    webClient.Headers.Add("Authorization", "Client-ID " + ClientId);

                    byte[] response = webClient.UploadValues(ApiUrl, values);

                    var streamReader = new StreamReader(new MemoryStream(response));

                    while (streamReader.Peek() >= 0)
                    {
                        var line = streamReader.ReadLine();

                        if (line != null && line.Contains("link"))
                        {
                            line = line.Substring(line.IndexOf(":", StringComparison.Ordinal) - 4, line.Length - line.IndexOf(":", StringComparison.Ordinal));
                            line = line.Substring(0, line.IndexOf("<", StringComparison.Ordinal));

                            return line;
                        }
                    }

                    streamReader.Dispose();
                }
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }

            return null;
        }
    }
}