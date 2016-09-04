using System;
using System.Web.UI;
using Zhigly.Code;
using Zhigly.Code.API;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string thankyou = Page.RouteData.Values["thankyou"] as string;

            if (!string.IsNullOrEmpty(thankyou))
            {
                ContactForm.Visible = false;
                Sent.Visible = true;
                return;
            }

            ContactForm.Visible = true;
            Sent.Visible = false;

            if (!Authentication.IsActive())
            {
                return;
            }

            User user = Database.GetUser(Authentication.GetEmail());

            if (user == null)
            {
                return;
            }

            Name.Text = user.GetDisplayName();
            Name.Enabled = false;

            Email.Text = user.Email;
            Email.Enabled = false;
        }

        protected void SendMessage_Click(object sender, EventArgs e)
        {
            string name = Name.Text;
            string email = Email.Text;
            string reason = string.IsNullOrEmpty(Reason.SelectedValue) ? "Other" : Reason.SelectedValue;
            string message = Message.Text;

            if (!IsDataValid(name, email, reason, message))
            {
                return;
            }

            string body = GetContactBody(name, email, reason, message);
            
            bool success = Zoho.Send(Zoho.Email, "Contact message from " + name, body, false);

            if (!success)
            {
                ShowError("There was an error receiving your message. Please try again.");
                return;
            }

            Response.Redirect("~/Contact.aspx/ThankYou");
        }

        private static string GetContactBody(string name, string email, string reason, string message)
        {
            const string newline = "\r\n";

            string body = "";

            body += "Name: " + name + newline;
            body += "Email: " + email + newline;
            body += "Reason: " + reason + newline;
            body += "Message: " + message + newline;

            return body;
        }

        protected bool IsDataValid(string name, string email, string reason, string message)
        {
            string[] validations = {
                Validation.ValidateName(name),
                Validation.ValidateEmail(email),
                Validation.ValidateReason(reason),
                Validation.ValidateMessage(message)
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
            ErrorLabel.InnerText = error;
        }
    }
}