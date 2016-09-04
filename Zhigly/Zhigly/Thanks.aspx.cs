using System;
using System.Web.UI;
using Zhigly.Code;

namespace Zhigly
{
    public partial class Thanks : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authentication.IsActive())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            string email = (string)Session["Email"];

            if (string.IsNullOrEmpty(email))
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            ThankYouMessage.InnerText = "Please verify your email with the code we sent to " + email + ".";
        }
    }
}