using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Settings : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Authentication.IsActive())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            User user = Database.GetUser(Authentication.GetEmail());

            if (user == null)
            {
                Authentication.LogOut();
                return;
            }

            LeftHeader.InnerText = "Settings";
            AddLeftMenuLinks();

            if (Page.IsPostBack)
            {
                return;
            }

            string mode = Page.RouteData.Values["mode"] as string;

            Default.Visible = false;
            Info.Visible = false;
            Listings.Visible = false;
            ChangePassword.Visible = false;

            if (string.IsNullOrEmpty(mode))
            {
                ShowDefault();
            }
            else if (string.Equals(mode, "info", StringComparison.OrdinalIgnoreCase))
            {
                ShowBasicInformation(user);
            }
            else if (string.Equals(mode, "listings", StringComparison.OrdinalIgnoreCase))
            {
                ShowListings(user);
            }
            else if (string.Equals(mode, "password", StringComparison.OrdinalIgnoreCase))
            {
                ShowChangePassword();
            }
            else
            {
                Response.Redirect("~/Settings.aspx");
            }
        }

        private void ShowChangePassword()
        {
            ChangePassword.Visible = true;
            RightHeader.InnerText = "Change Password";
        }

        private void ShowListings(User user)
        {
            Listings.Visible = true;
            RightHeader.InnerText = "My Listings";

            List<Advertisement> ads = Database.GetUserAds(user.Id);

            if (ads == null)
            {
                return;
            }

            foreach (Advertisement ad in ads)
            {
                AddAdvertisement(ad);
            }
        }
        
        private void AddAdvertisement(Advertisement ad)
        {

            HtmlGenericControl outer = new HtmlGenericControl("div");
            outer.Attributes.Add("class", "ad" + (ad.Boosted ? " boost" : ""));

            HtmlGenericControl title = new HtmlGenericControl("div");
            title.Attributes.Add("class", "title");
            title.InnerHtml = "<h2>" + ad.Title + "</h2>";
            outer.Controls.Add(title);

            HtmlGenericControl posted = new HtmlGenericControl("div");
            posted.Attributes.Add("class", "created");
            posted.InnerHtml = "<p>Posted " + (ad.Anonymous ? "<strong>anonymously</strong>" : "") + " on " + ad.GetCreationDate() + " and has " + ad.Views + " views</p>";

            outer.Controls.Add(posted);

            HtmlGenericControl description = new HtmlGenericControl("div");
            description.Attributes.Add("class", "description");
            description.InnerHtml = "<h3>" + ad.Description + "</h3>";
            outer.Controls.Add(description);

            HyperLink hyperlink = new HyperLink();
            hyperlink.Text = "Edit Listing >>";
            hyperlink.NavigateUrl = "~/Edit/" + ad.Id;
            outer.Controls.Add(hyperlink);

            Listings.Controls.Add(outer);
        }

        private void ShowBasicInformation(User user)
        {
            Info.Visible = true;

            SchoolTextbox.Text = user.School.Name;
            SchoolTextbox.Enabled = false;

            EmailTextbox.Text = user.Email;
            EmailTextbox.Enabled = false;

            PrimaryNameTextbox.Text = user.PrimaryName;
            SecondaryNameTextbox.Text = user.SecondaryName;
            PhoneTextbox.Text = user.PhoneNumber.Length > 1 ? user.PhoneNumber : "";

            RightHeader.InnerText = "Basic Information";

            if (user.Organization)
            {
                PrimaryNameLabel.InnerHtml = "Organization Name:";
                SecondaryNameLabel.InnerHtml = "Position:";
            }
        }

        private void ShowDefault()
        {
            Default.Visible = true;
            RightHeader.InnerText = "Settings";
        }

        private void AddLeftMenuLinks()
        {
            AddLeftMenuHyperLink("Basic Information", "~/Settings/Info");
            AddLeftMenuHyperLink("My Listings", "~/Settings/Listings");
            AddLeftMenuHyperLink("Change Password", "~/Settings/Password");
        }

        private void AddLeftMenuHyperLink(string title, string url)
        {
            HyperLink hyperlink = new HyperLink();
            hyperlink.Text = title;
            hyperlink.NavigateUrl = url;

            LeftSection.Controls.Add(hyperlink);
        }

        protected void saveButton_Click(object sender, EventArgs e)
        {
            if (!Authentication.IsActive())
            {
                Response.Redirect("~/");
            }

            User user = Database.GetUser(Authentication.GetEmail());

            if (user == null)
            {
                Authentication.LogOut();
                return;
            }

            string primaryName = PrimaryNameTextbox.Text;
            string secondaryName = SecondaryNameTextbox.Text;
            string phone = PhoneTextbox.Text;

            if (!IsDataValid(primaryName, secondaryName, phone, user.Organization))
            {
                return;
            }

            if (Database.UpdateUserInfo(user.Email, primaryName, secondaryName, phone))
            {
                ShowError("Your information has been saved successfully.");
            }
        }

        protected bool IsDataValid(string primary, string secondary, string phone, bool organization)
        {
            string[] validations = {
                Validation.ValidatePrimaryName(primary, organization),
                Validation.ValidateSecondaryName(secondary, organization),
                Validation.ValidatePhoneNumber(phone)
            };

            foreach (string error in validations)
            {
                if (error != null)
                {
                    ShowError(error);
                    return false;
                }
            }

            return true;
        }

        protected void ShowError(string error)
        {
            InfoError.InnerText = error;
        }

        protected void changePassword_Click(object sender, EventArgs e)
        {
            if (!Authentication.IsActive())
            {
                Response.Redirect("~/");
            }

            User user = Database.GetUser(Authentication.GetEmail());

            if (user == null)
            {
                Authentication.LogOut();
                return;
            }

            string currentPassword = CurrentPasswordTextbox.Text;
            string newPassword = NewPasswordTextbox.Text;
            string confirmPassword = ConfirmPasswordTextbox.Text;

            if (!changePassword(user.Password, currentPassword, newPassword, confirmPassword))
            {
                return;
            }

            if (Database.UpdateUserPassword(user.Email, newPassword))
            {
                ShowPasswordError("Your password has been changed successfully.");
            }

        }

        private bool changePassword(string userPassword, string currentPassword, string newPassword, string confirmPassword)
        {
            string error = Validation.ValidateChangePassword(userPassword, currentPassword, newPassword, confirmPassword);

            if (error != null)
            {
                ShowPasswordError(error);
                return false;
            }
            
            return true;
        }

        protected void ShowPasswordError(string error)
        {
            PasswordError.InnerText = error;
        }
    }
}