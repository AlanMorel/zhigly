using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Activation : Page
    {
        private const string Red = "#c60b1e";
        private const string Green = "#27ae60";


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Authentication.IsActive())
            {
                Response.Redirect("~/");
                return;
            }

            string values = Page.RouteData.Values["code"] as string;

            if (string.IsNullOrEmpty(values))
            {
                Response.Redirect("~/");
                return;
            }

            string[] data = values.Split('-');

            if (data.Length != 2)
            {
                Response.Redirect("~/");
                return;
            }

            string id = data[0];
            string code = data[1];

            int idInt;

            if (!int.TryParse(id, out idInt))
            {
                Response.Redirect("~/");
                return;
            }

            int codeInt;

            if (!int.TryParse(code, out codeInt))
            {
                Response.Redirect("~/");
                return;
            }

            if (idInt < 0 || codeInt < 0)
            {
                Response.Redirect("~/");
                return;
            }

            User user = Database.GetUser(idInt);

            if (user == null)
            {
                Response.Redirect("~/");
                return;
            }

            Success.Visible = false;
            Failure.Visible = false;
            Activated.Visible = false;
            
            if (user.IsEmailVerified())
            {
                Activated.Visible = true;
                SetCustomColor(Green);
                return;
            }
            
            if (user.Status != codeInt)
            {
                Failure.Visible = true;
                return;
            }
            
            if (!Database.VerifyEmail(user.Id))
            {
                Failure.Visible = true;
                return;
            }

            Name.InnerHtml = user.PrimaryName;
            Success.Visible = true;
            SetCustomColor(Green);
        }

        private void SetCustomColor(string color)
        {
            Style style = new Style();
            style.BackColor = ColorTranslator.FromHtml(color);

            Page.Header.StyleSheet.CreateStyleRule(style, null, "body");
        }
    }
}