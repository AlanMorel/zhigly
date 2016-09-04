using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.API;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Authentication.IsActive())
            {
                Response.Redirect("~/");
                return;
            }

            if (IsPostBack)
            {
                return;
            }

            RecaptchaDiv.Attributes.Add("class", "g-recaptcha");
            RecaptchaDiv.Attributes.Add("data-sitekey", Recaptcha.SiteKey);

            foreach (College school in College.GetColleges())
            {
                EmailDropdown.AddItemGroup(school.Name);

                foreach (string email in school.Emails)
                {
                    EmailDropdown.Items.Add(new ListItem("@" + email, school.Id.ToString()));
                }
            }
        }

        protected void registerButton_Click(object sender, EventArgs e)
        {
            int schoolId;
            
            if (!int.TryParse(EmailDropdown.SelectedValue, out schoolId))
            {
                ShowError("Please select a school.");
                return;
            }

            College school = College.Get(schoolId);

            if (school == null)
            {
                ShowError("Please select a school.");
                return;
            }

            string email = EmailTextbox.Text.ToLower() + "@" + school.ShortName.ToLower() + ".edu";
            string primaryName = PrimaryNameTextbox.Text;
            string secondaryName = SecondaryNameTextbox.Text;
            string password = PasswordTextbox.Text;
            string confirmation = ConfirmPasswordTextbox.Text;
            bool organization = TypeRadioButtonList.SelectedIndex == 3;

            if (!IsDataValid(email, primaryName, secondaryName, password, confirmation, organization))
            {
                return;
            }

            int verification = GenerateVerificationCode();

            Debug.WriteLine(verification);

            int userId = Database.RegisterUser(school, email, primaryName, secondaryName, password, organization, verification);

            if (userId < 0)
            {
                ShowError("An error has occurred. Please try again.");
                return;
            }

            if (!Zoho.SendActivationEmail(email, primaryName, userId, verification))
            {
                ShowError("An error has occurred. Please try again.");
                return;
            }

            school.Users++;
            Session["Email"] = email;
            Response.Redirect("~/Thanks.aspx");

        }

        protected bool IsDataValid(string email, string primary, string secondary, string password, string confirmation, bool organization)
        {
            string[] validations = {
                Validation.ValidateEmail(email),
                Validation.ValidatePrimaryName(primary, organization),
                Validation.ValidateSecondaryName(secondary, organization),
                Validation.ValidatePasswords(password, confirmation),
                Database.IsEmailFree(email)
            };

            foreach (string error in validations)
            {
                if (error != null)
                {
                    ShowError(error);
                    return false;
                }
            }

            if (!ValidReCaptcha())
            {
                ShowError("The ReCaptcha was invalid.");
                return false;
            }

            return true;
        }

        public bool ValidReCaptcha()
        {
            string response = Request["g-recaptcha-response"];
            string url = Recaptcha.GetURL(response);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            try
            {
                using (WebResponse wResponse = request.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string json = readStream.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Recaptcha data = js.Deserialize<Recaptcha>(json);

                        return Convert.ToBoolean(data.Success);
                    }
                }
            }
            catch (WebException exception)
            {
                Utility.Log(exception);
            }

            return false;
        }

        protected void ShowError(string error)
        {
            ErrorLabel.InnerText = error;
        }

        private int GenerateVerificationCode()
        {
            const int digits = 6;

            int min = (int) Math.Pow(10, digits);
            int max = (int) Math.Pow(10, digits + 1) - 1;
                
            return new Random().Next(min, max);
        }
    }
}