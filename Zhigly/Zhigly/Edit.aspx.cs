using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.API;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Edit : Page
    {
        private const int UploadNew = 0;
        private const int UseCurrent = 1;
        private const int NoPicture = 2;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Authentication.IsActive())
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            string id = Page.RouteData.Values["id"] as string;

            if (string.IsNullOrEmpty(id))
            {
                Response.Redirect("~/");
                return;
            }

            int idInt;


            if (!int.TryParse(id, out idInt))
            {
                Response.Redirect("~/");
                return;
            }
            Advertisement ad = Database.GetAd(idInt);

            if (ad == null)
            {
                Response.Redirect("~/");
                return;
            }

            User user = Database.GetUser(Authentication.GetEmail());

            if (user == null)
            {
                Authentication.LogOut();
                return;
            }

            if (ad.User != user.Id || ad.Deleted)
            {
                Response.Redirect("~/");
                return;
            }

            DeletePrompt.Style.Add("display", "none");

            SetImages(ad);

            if (IsPostBack)
            {
                return;
            }

            SetListing(ad);
        }

        private void SetListing(Advertisement ad)
        {
            if (GroupedDropDownList.Items.Count == 0)
            {
                foreach (Category category in Category.GetCategories())
                {
                    GroupedDropDownList.AddItemGroup(category.Name);
                    foreach (Subcategory subcategory in category.Subcategories)
                    {
                        GroupedDropDownList.Items.Add(new ListItem(subcategory.Name, category.Id + "-" + subcategory.Id.ToString()));
                    }
                }
            }

            TitleTextBox.Text = ad.Title;
            GroupedDropDownList.SelectedValue = ad.Category + "-" + ad.Subcategory;
            DescriptionTextBox.Text = ad.Description;

            if (ad.IsExpired())
            {
                TitleTextBox.Enabled = false;
                GroupedDropDownList.Enabled = false;
                DescriptionTextBox.Enabled = false;
                EditButtons.Visible = false;

                ExpirationText.InnerText = "This listing expired on " + ad.GetExpirationDate() + ".";

                if (ad.Anonymous)
                {
                    VisiblityText.InnerHtml = "This listing was anonymous.";
                }
                else
                {
                    VisiblityText.InnerHtml = "This listing was not anonymous.";
                }

                if (ad.Boosted)
                {
                    BoostText.InnerHtml = "This listing was boosted.";
                }
                else
                {
                    BoostText.InnerHtml = "This listing was not boosted.";
                }

            }
            else
            {
                ExpirationText.InnerText = "This listing expires on " + ad.GetExpirationDate() + ".";

                if (ad.Anonymous)
                {
                    VisiblityText.InnerHtml = "This listing is anonymous.";
                }
                else
                {
                    VisiblityText.InnerHtml = "This listing is not anonymous.";
                }

                if (ad.Boosted)
                {
                    BoostText.InnerHtml = "This listing is boosted.";
                }
                else
                {
                    BoostText.InnerHtml = "This listing is not boosted.";
                }

            }

            if (ad.Anonymous)
            {
                Visibility.Attributes["class"] = "option-container invisible";
                Spy.Attributes["class"] = "spy white";
            }
            else
            {
                Visibility.Attributes["class"] = "option-container visible";
                Spy.Attributes["class"] = "spy black";
            }

            if (ad.Boosted)
            {
                Boost.Attributes["class"] = "option-container enabled";
                Star.Attributes["class"] = "star enabled-star";
            }
            else
            {
                Boost.Attributes["class"] = "option-container disabled";
                Star.Attributes["class"] = "star disabled-star";
            }
        }


        private void SetImages(Advertisement ad)
        {
            for (int i = 0; i < Constants.ImagesPerListing; i++)
            {
                if (i < ad.Images.Count)
                {
                    SetImage(i + 1, ad.Images[i], true);
                }
                else
                {
                    SetImage(i + 1, null, false);
                }
            }
        }

        private void SetImage(int id, string url, bool visible)
        {
            HtmlGenericControl imageSection = new HtmlGenericControl("div");
            imageSection.Attributes.Add("class", "image-section");

            HtmlGenericControl title = new HtmlGenericControl("div");
            title.Attributes.Add("class", "title");
            title.InnerText = "Image " + id;

            HyperLink image = new HyperLink();
            image.NavigateUrl = url;
            image.ImageUrl = url;
            image.Attributes.Add("data-lightbox", "image");
            Images.Controls.Add(image);

            FileUpload fileUpload = new FileUpload();
            fileUpload.ID = "FileUpload" + id;

            RadioButtonList radioList = new RadioButtonList();
            radioList.Items.Add(new ListItem("Upload New", "0"));
            radioList.Items.Add(new ListItem("Use Current", "1"));
            radioList.Items.Add(new ListItem("No Picture", "2"));

            radioList.SelectedIndex = url == null ? UploadNew : UseCurrent;

            imageSection.Controls.Add(title);
            imageSection.Controls.Add(image);
            imageSection.Controls.Add(fileUpload);
            imageSection.Controls.Add(radioList);

            imageSection.Attributes["style"] = "display: " + (visible ? "block" : "none");

            Images.Controls.Add(imageSection);
        }

        protected void Yes_Click(object sender, EventArgs e)
        {
            DeletePrompt.Style.Add("display", "none");

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

            string id = Page.RouteData.Values["id"] as string;

            if (string.IsNullOrEmpty(id))
            {
                Response.Redirect("~/");
                return;
            }

            Advertisement ad = Database.GetAd(int.Parse(id));

            if (ad == null || ad.User != user.Id)
            {
                Response.Redirect("~/");
                return;
            }

            if (Database.DeleteAd(ad.Id))
            {
                Response.Redirect("~/Settings.aspx/Listings");
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        protected void SaveListing_Click(object sender, EventArgs e)
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

            string id = Page.RouteData.Values["id"] as string;

            if (string.IsNullOrEmpty(id))
            {
                Response.Redirect("~/");
                return;
            }

            Advertisement ad = Database.GetAd(int.Parse(id));

            if (ad == null)
            {
                Response.Redirect("~/");
                return;
            }

            if (ad.User != user.Id)
            {
                Response.Redirect("~/");
                return;
            }

            string title = TitleTextBox.Text;
            string[] categories = GroupedDropDownList.SelectedValue.Split('-');
            int category = int.Parse(categories[0]);
            int subcategory = int.Parse(categories[1]);
            string description = DescriptionTextBox.Text.Replace(Environment.NewLine, "<br />");

            List<string> added = new List<string>();
            List<string> deleted = new List<string>();

            SortImages(ad, added, deleted);

            UpdateAdvertisement(ad, title, category, subcategory, description, added, deleted);
        }

        private void UpdateAdvertisement(Advertisement ad, string title, int category, int subcategory, string description, List<string> added, List<string> deleted)
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

            bool success = Database.UpdateAd(ad.Id, title, category, subcategory, description, added, deleted);

            if (success)
            {
                Response.Redirect("~/Listing/" + ad.Id);
            }
            else
            {
                ShowError("An error has occured. Please try again.");
            }
        }

        protected void ShowError(string error)
        {
            ErrorLabel.InnerText = error;
        }

        private void SortImages(Advertisement ad, List<string> added, List<string> deleted)
        {
            List<RadioButtonList> radios = Images.GetAll<RadioButtonList>();
            List<FileUpload> fileUploads = Images.GetAll<FileUpload>();

            for (int i = 0; i < Constants.ImagesPerListing; i++)
            {
                try
                {
                    if (radios[i].SelectedIndex == UploadNew)
                    {
                        byte[] data = GetImage(fileUploads[i]);

                        if (data == null || data.Length <= 0)
                        {
                            continue;
                        }

                        string url = Imgur.GetUrl(data);

                        if (string.IsNullOrEmpty(url))
                        {
                            continue;
                        }

                        deleted.Add(ad.Images[i]);
                        added.Add(url);
                    }
                    else if (radios[i].SelectedIndex == NoPicture)
                    {
                        deleted.Add(ad.Images[i]);
                    }

                } catch { }
            }
        }

        private byte[] GetImage(FileUpload fileUpload)
        {
            try
            {
                Stream stream = fileUpload.PostedFile.InputStream;

                if (stream.Length == 0 || stream.Length > 2 << 21)
                {
                    return null;
                }

                return new BinaryReader(stream).ReadBytes((int)stream.Length);

            }
            catch
            {
                // ignored
            }

            return null;
        }
    }
}