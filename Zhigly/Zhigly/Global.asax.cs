using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Zhigly.Code;
using Zhigly.Code.API;

namespace Zhigly
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            SetRoutes();

            Database.SetSchoolStats();

            Facebook.Likes = 0; //Utility.GetFacebookLikes();
            Twitter.Followers = 0; // Utility.GetTwitterFollowers();
            Plus.Followers = 0; //Plus.GetFollowers();
        }
        
        protected void SetRoutes()
        {
            RouteTable.Routes.LowercaseUrls = true;

            RouteTable.Routes.MapPageRoute("ListingRoute", "Listing/{id}", "~/Listing.aspx");
            RouteTable.Routes.MapPageRoute("EditRoute", "Edit/{id}", "~/Edit.aspx");

            RouteTable.Routes.MapPageRoute("SchoolRoute1", "School/{name}/", "~/School.aspx");
            RouteTable.Routes.MapPageRoute("SchoolRoute2", "School/{name}/{filter}", "~/School.aspx");
            RouteTable.Routes.MapPageRoute("SchoolRoute3", "School/{name}/{filter}/{info1}", "~/School.aspx");
            RouteTable.Routes.MapPageRoute("SchoolRoute4", "School/{name}/{filter}/{info1}/{info2}", "~/School.aspx");

            RouteTable.Routes.MapPageRoute("SettingsRoute", "Settings/{mode}/", "~/Settings.aspx");

            RouteTable.Routes.MapPageRoute("ContactRoute", "Contact/{thankyou}/", "~/Contact.aspx");

            RouteTable.Routes.MapPageRoute("BlogRoute", "Blog/{post}/", "~/Blog.aspx");

            RouteTable.Routes.MapPageRoute("ActivationRoute", "Activation/{code}", "~/Activation.aspx");

            FriendlyUrlSettings settings = new FriendlyUrlSettings
            {
                AutoRedirectMode = RedirectMode.Permanent
            };

            RouteTable.Routes.EnableFriendlyUrls(settings);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Regex.IsMatch(HttpContext.Current.Request.RawUrl, @"[A-Z]"))
            {
                string lowercaseUrl = HttpContext.Current.Request.RawUrl.ToLower();
                Response.Clear();
                Response.Status = "301 Moved Permanently";
                Response.AddHeader("Location", lowercaseUrl);
                Response.End();
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            string message = exception.Message;
            string stacktrace = exception.StackTrace;

            Database.Log(stacktrace);
            Debug.WriteLine("Stack trace logged to database:");
            Debug.WriteLine(message);
            Debug.WriteLine(stacktrace);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}