using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Zhigly.Code;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authentication.IsActive())
            {
                Response.Redirect("~/");
                return;
            }

            HttpCookie credentials = Request.Cookies["Zhigly"];

            if (credentials == null)
            {
                return;
            }

            string email = credentials.Values["email"];
            string password = credentials.Values["password"];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return;
            }

            EmailTextbox.Text = email;
            PasswordTextbox.Text = password;

        }

        protected void logInButton_Click(object sender, EventArgs e)
        {
            string email = EmailTextbox.Text.ToLower();
            string password = PasswordTextbox.Text;
            bool rememberMe = RememberMeCheckbox.Checked;

            if (!Database.AuthenticateUser(email, password))
            {
                ShowError("You entered an invalid username or password. Please try again.");
                return;
            }

            User user = Database.GetUser(email);

            if (user == null)
            {
                ShowError("You entered an invalid username or password. Please try again.");
                return;
            }

            if (user.IsBanned())
            {
                ShowError("Your account has been banned. Please contact us for further information.");
                return;
            }

            if (rememberMe)
            {
                HttpCookie credentials = new HttpCookie("Zhigly");

                credentials.Values.Add("email", email);
                credentials.Values.Add("password", password);

                Response.Cookies.Add(credentials);
            }

            Database.UpdateLastLogin(email);

            HttpCookie name = new HttpCookie("Name");
            name.Value = user.PrimaryName;
            name.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(name);

            HttpCookie school = new HttpCookie("School");
            school.Value = (user.School.Id).ToString();
            school.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(school);

            FormsAuthentication.RedirectFromLoginPage(email, RememberMeCheckbox.Checked);
        }

        protected void ShowError(string error)
        {
            ErrorLabel.InnerText = error;
        }
    }
}