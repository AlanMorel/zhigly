using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.API;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Post : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                return;
            }

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

            UserCredits.InnerText = user.Credit.ToString() + " ";

            if (GroupedDropDownList.Items.Count == 0)
            {
                foreach (Category category in Category.GetCategories())
                {
                    GroupedDropDownList.AddItemGroup(category.Name);

                    foreach (Subcategory subcategory in category.Subcategories)
                    {
                        GroupedDropDownList.Items.Add(new ListItem(subcategory.Name, category.Id.ToString() + "-" + subcategory.Id.ToString()));
                    }
                }
            }
            
            if (DurationDropDownList.Items.Count == 0)
            {
                DurationDropDownList.Items.Add(new ListItem("30 Days", "30"));
                DurationDropDownList.Items.Add(new ListItem("14 Days", "14"));
                DurationDropDownList.Items.Add(new ListItem("7 Days", "7"));
                DurationDropDownList.Items.Add(new ListItem("3 Days", "3"));
                DurationDropDownList.Items.Add(new ListItem("1 Day", "1"));
            }

            for (int i = 0; i < Constants.ImagesPerListing; i++)
            {
                FileUpload fileUpload = new FileUpload();
                fileUpload.Attributes["style"] = "display: none";

                FileUploads.Controls.Add(fileUpload);
            }

        }

        protected void ShowError(string error)
        {
            ErrorLabel.InnerText = error;
        }

        protected void PostListing_Click(object sender, EventArgs e)
        {

            if (!Authentication.IsActive())
            {
                Authentication.LogOut();
                return;
            }

            User user = Database.GetUser(Authentication.GetEmail());

            if (user == null)
            {
                Authentication.LogOut();
                return;
            }

            if (!user.IsEmailVerified())
            {
               ShowError("You must verify your email before posting. Check your inbox for a verification code.");
               return;
            }

            string title = TitleTextBox.Text;
            string[] categories = GroupedDropDownList.SelectedValue.Split('-');
            int category = int.Parse(categories[0]);
            int subcategory = int.Parse(categories[1]);
            string description = DescriptionTextBox.Text.Replace(Environment.NewLine, "<br />");
            bool anonymous = AnonymityCheckbox.Checked;
            bool boosted = BoostCheckbox.Checked;
            int duration = int.Parse(DurationDropDownList.SelectedValue);

            PostAdvertisement(user, title, category, subcategory, description, anonymous, boosted, duration);
        }

        private int GetCost(int duration, bool anonymous, bool boosted)
        {
            Debug.WriteLine("Duration: " + duration + " Anony: " + anonymous + " Boost: " + boosted);
            return duration * ((anonymous ? 5 : 0) + (boosted ? 10 : 0));
        }

        private void PostAdvertisement(User user, string title, int category, int subcategory, string description, bool anonymous, bool boosted, int days)
        {
            string[] validations = {
                Validation.ValidateTitle(title),
                Validation.ValidateDescription(description)
            };

            foreach (string error in validations)
            {
                if (error != null)
                {
                    ShowError(error);
                    return;
                }
            }

            int cost = GetCost(days, anonymous, boosted);

            if (user.Credit < cost)
            {
                ShowError("You do not have enough credit in your account to post this listing.");
                return;
            }

            List<string> images = GetImages();

            foreach (string image in images)
            {
                Debug.WriteLine("URL: " + image);
            }

            DateTime expiration = DateTime.Now.AddDays(days);

            int listingId = Database.PostAd(user, title, category, subcategory, description, images, expiration, anonymous, boosted);
            
            Database.MakePurchase(user, listingId, anonymous, boosted, days, cost);

            if (listingId > 0)
            {
                user.School.Listings++;
                Response.Redirect("~/Listing/" + listingId);
            }
            else
            {
                ShowError("An error has occured. Please try again.");
            }
        }

        private List<string> GetImages()
        {
            List<string> images = new List<string>();

            for (int index = 0; index < Request.Files.Count; index++)
            {
                HttpPostedFile file = Request.Files[index];

                string extension = Path.GetExtension(file.FileName);

                try
                {
                    if (string.IsNullOrEmpty(extension))
                    {
                        continue;
                    }

                    Stream stream = file.InputStream;

                    if (stream.Length == 0 || stream.Length > 2 << 22)
                    {
                        continue;
                    }

                    byte[] data = new BinaryReader(stream).ReadBytes((int)stream.Length);

                    string url = Imgur.GetUrl(data);

                    if (string.IsNullOrEmpty(url))
                    {
                        continue;
                    }

                    images.Add(url);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return images;
        }
    }
}