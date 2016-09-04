using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Listing : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            ListingId.Value = id;

            College school = College.Get(ad.School);

            if (school == null)
            {
                Response.Redirect("~/");
                return;
            }

            SetBannerAndTitle(school.ShortName, id);
            SetPageMenu(school.Name, school.ShortName);
            SetCustomHeaderColor(school);

            if (ad.Deleted)
            {
                AdTitle.Attributes["class"] = "deleted";
                AdTitle.InnerText = "This listing has been deleted.";
                AdBody.Visible = false;
                return;
            }

            if (ad.IsExpired())
            {
                AdTitle.Attributes["class"] = "expiration";
                AdTitle.InnerText = "This listing has expired.";
                AdBody.Visible = false;
                return;
            }

            User owner = Database.GetUser(ad.User);

            if (owner == null)
            {
                Response.Redirect("~/");
                return;
            }

            SetPageContent(ad, owner);
            SetListingImages(ad);
            SetReportReasons();

            AddSocialMedia(id);

            Database.AddListingView(ad.Id);
        }

        private static bool IsOwner(Advertisement ad)
        {
            if (!Authentication.IsActive())
            {
                return false;
            }

            User user = Database.GetUser(Authentication.GetEmail());

            if (user == null)
            {
                return false;
            }

            return user.Id == ad.User;
        }

        private void SetPageContent(Advertisement ad, User user)
        {
            if (IsOwner(ad))
            {
                Owner.Attributes["style"] = "display: block";
                Owner.InnerHtml = "<a href='../edit/" + ad.Id + "'>This is your listing. If you would like to edit it, click here</a>.";
                Report.Visible = false;
            }
            
            Category category = Category.Get(ad.Category);

            Subcategory subcategory = category.GetSubcategory(ad.Subcategory);

            PostDate.InnerHtml = "[" + (subcategory != null ? subcategory.Name : category.DisplayName) + "] Posted " + Utility.GetRelativeTimeString(ad.Created) + (ad.Anonymous ? "" : " by " + user.GetDisplayName());

            if (user.IsAccountVerified())
            {
                HtmlGenericControl verified = new HtmlGenericControl("span");
                verified.Attributes.Add("class", "verified");
                verified.Attributes.Add("title", ad.Anonymous ? "This user is verified." : user.GetDisplayName() + " is a verified user.");
                PostDate.Controls.Add(verified);
            }

            if (ad.Boosted)
            {
                HtmlGenericControl verified = new HtmlGenericControl("span");
                verified.Attributes.Add("class", "boost");
                verified.Attributes.Add("title", "This listing is boosted.");
                PostDate.Controls.Add(verified);
            }

            if (ad.Views >= Constants.PopularityThreshold)
            {
                HtmlGenericControl popular = new HtmlGenericControl("span");
                popular.Attributes.Add("class", "popular");
                popular.Attributes.Add("title", "This listing is really popular.");
                PostDate.Controls.Add(popular);
            }

            ViewCount.InnerHtml = "Views: " + (ad.Views + 1);

            AdTitle.InnerText = ad.Title;
            Description.InnerHtml = ad.Description;
        }


        private void SetReportReasons()
        {
            foreach (ReportReason reason in ReportReason.Get())
            {
                Reason.Items.Add(new ListItem(reason.Reason, reason.Id.ToString()));
            }
        }

        private void SetPageMenu(string schoolName, string schoolShortName)
        {
            LeftHeader.InnerText = schoolName;

            foreach (Filter filter in Filter.GetFilters())
            {
                HyperLink hyperlink = new HyperLink();
                hyperlink.Text = filter.Name;
                hyperlink.NavigateUrl = "~/School/" + schoolShortName + "/" + filter.Name;

                LeftSection.Controls.Add(hyperlink);
            }
        }

        private void SetBannerAndTitle(string schoolShortName, string id)
        {
            Banner.ImageUrl = "~/Images/SchoolBanner/" + schoolShortName + ".png";
            Page.Title = "Zhigly - Listing " + id + " at " + schoolShortName;
        }

        private void SetListingImages(Advertisement ad)
        {

            for (int i = 0; i < ad.Images.Count; i++)
            {
                HyperLink image = new HyperLink();
                image.NavigateUrl = ad.Images[i];
                image.ImageUrl = ad.Images[i];
                image.Attributes.Add("data-lightbox", "image");

                Images.Controls.Add(image);
            }
        }

        private void AddSocialMedia(string id)
        {
            HtmlGenericControl facebook = new HtmlGenericControl("div");
            facebook.Attributes.Add("class", "fb-like");
            facebook.Attributes.Add("data-href", "http://zhigly.com/listing/" + id);
            facebook.Attributes.Add("data-layout", "standard");
            facebook.Attributes.Add("data-action", "like");
            facebook.Attributes.Add("data-show-faces", "true");
            facebook.Attributes.Add("data-share", "true");

            HyperLink twitter = new HyperLink();
            twitter.NavigateUrl = "https://twitter.com/share";
            twitter.Attributes.Add("class", "twitter-share-button");
            twitter.Attributes.Add("data-text", "Check out this listing on Zhigly!");
            twitter.Attributes.Add("data-size", "large");
            
            HtmlGenericControl plus = new HtmlGenericControl("div");
            plus.Attributes.Add("class", "g-plusone");
            plus.Attributes.Add("data-href", "http://zhigly.com/listing/" + id);
            plus.Attributes.Add("data-annotation", "inline");
            plus.Attributes.Add("data-width", "650");
            
            HtmlGenericControl socialMedia = new HtmlGenericControl("div");
            socialMedia.Attributes.Add("class", "social-media");

            socialMedia.Controls.Add(facebook);
            socialMedia.Controls.Add(twitter);
            socialMedia.Controls.Add(plus);

            HtmlGenericControl comments = new HtmlGenericControl("div");
            comments.Attributes.Add("class", "fb-comments");
            comments.Attributes.Add("data-href", "http://zhigly.com/listing/" + id);
            comments.Attributes.Add("data-width", "660");
            comments.Attributes.Add("data-numposts", "5");

            RightSection.Controls.Add(socialMedia);
            CommentSection.Controls.Add(comments);
        }
        
        private void SetCustomHeaderColor(College school)
        {
            Style style = new Style();
            style.BackColor = ColorTranslator.FromHtml(school.Color);
            Page.Header.StyleSheet.CreateStyleRule(style, null, ".header");
        }
        
    }
}