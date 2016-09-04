using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Zhigly.Code.API
{
    public class Zoho
    {
        private const string Host = "smtp.zoho.com";
        private const int Port = 587;

        public const string Email = "Enter Zoho Email Here";
        private const string Password = "Enter Zoho Password Here";

        private const string Name = "Zhigly";

        private const string EmailLogoUrl = "http://i.imgur.com/VtRhu0z.png";

        public static bool SendActivationEmail(string destination, string name, int id, int verification)
        {
            string path = HttpContext.Current.Request.MapPath("~/ActivationProduction.html");

            StreamReader reader = new StreamReader(path);
            string body = reader.ReadToEnd();

            body = body.Replace("LOGO", EmailLogoUrl);
            body = body.Replace("ACTIVATION_LINK", "Activation.aspx/" + id + "-" + verification);
            body = body.Replace("USER", name);

            reader.Close();

            return Send(destination, "Zhigly Account Activation", body, true);
        }

        public static bool Send(string destination, string subject, string body, bool html)
        {
            try
            {
                SmtpClient client = GetClient();

                MailMessage email = new MailMessage
                {
                    Subject = subject,
                    Body = body,
                    From = new MailAddress(Email, Name),
                    IsBodyHtml = html
                };

                email.To.Add(new MailAddress(destination));

                client.Send(email);

                return true;
            }
            catch (Exception exception)
            {
                Utility.Log(exception);
            }

            return false;
        }

        private static SmtpClient GetClient()
        {
            SmtpClient client = new SmtpClient(Host, Port)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(Email, Password)
            };

            return client;
        }
    }
}