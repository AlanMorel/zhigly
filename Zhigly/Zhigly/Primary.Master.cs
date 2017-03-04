using System;
using System.Web;
using System.Web.UI;
using Zhigly.Code;
using Zhigly.Code.API;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Primary : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.IsActive())
            {
                LoggedOutHeader.Visible = true;
                LoggedInHeader.Visible = false;

                NamePrompt.InnerHtml = "Hello, Guest";
            }
            else
            {
                LoggedOutHeader.Visible = false;
                LoggedInHeader.Visible = true;

                Browse.Text = "Browse" + GetSchool();
                NamePrompt.InnerHtml = GetGreeting() + GetName();

                User user = Database.GetUser(Authentication.GetEmail());

                if (user != null)
                {
                    Browse.Attributes.Add("href", "/school/" + user.School.ShortName);
                }
                else
                {
                    Browse.Attributes.Add("href", "/schools");
                }
            }

            FacebookLikes.InnerHtml = "(" + Facebook.Likes + " likes)";
            TwitterFollowers.InnerHtml = "(" + Twitter.Followers + " followers)";
            PlusFollowers.InnerHtml = "(" + Plus.Followers + " followers)";
            Year.InnerHtml = DateTime.Now.Year.ToString();
        }

        private string GetName()
        {
            string name = GetCookieData("Name");

            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            return ", " + name;
        }

        private string GetSchool()
        {
            string school = GetCookieData("School");

            if (!string.IsNullOrEmpty(school))
            {
                College schoolObject = College.Get(int.Parse(school));

                if (schoolObject != null)
                {
                    return " " + schoolObject.ShortName;
                }
            }

            return string.Empty;
        }

        private string GetCookieData(string key)
        {
            if (!Request.Browser.Cookies)
            {
                return string.Empty;
            }

            HttpCookie httpCookie = Request.Cookies[key];

            if (httpCookie == null)
            {
                return string.Empty;
            }

            string data = httpCookie.Value;

            if (!string.IsNullOrEmpty(data))
            {
                return data;
            }

            return string.Empty;
        }

        private string GetGreeting()
        {
            if (DateTime.Now.Hour < 12)
            {
                return "Good morning";
            }

            if (DateTime.Now.Hour < 17)
            {
                return "Good afternoon";
            }

            return "Good evening";
        }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Authentication.LogOut();
        }
    }
}