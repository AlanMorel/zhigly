using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Authentication.IsActive())
            {
                LoggedOut.Visible = true;
                LoggedIn.Visible = false;
                return;
            }

            College school = GetSchool();

            if (school == null)
            {
                Authentication.LogOut();
                return;
            }

            LoggedOut.Visible = false;
            LoggedIn.Visible = true;

            SetCustomHeaderColor(school);
            SetCustomBackground(school);
            SchoolName.InnerHtml = school.Name;

            DisplayFilters(school);

            DisplayRecentListings(school);
            DisplayRecentBlogPosts();

            DisplayPopularListings(school);
            DisplayPopularBoostedListings(school);

            DisplayFeatured(school);

        }

        private void DisplayFeatured(College school)
        {
            int featured = school.GetFeaturedListing();

            if (featured < 0)
            {
                return;
            }

            Advertisement ad = Database.GetAd(featured);

            if (ad == null || ad.Deleted || ad.IsExpired() || !ad.HasAnImage())
            {
                return;
            }

            DisplayFeaturedListing(ad);
        }

        private void DisplayFeaturedListing(Advertisement ad)
        {
            HtmlGenericControl imageContainer = new HtmlGenericControl("div");
            imageContainer.Attributes.Add("class", "featured-image-container");

            Image image = new Image
            {
                ImageUrl = ad.GetRandomImage(),
                CssClass = "featured-image"
            };

            imageContainer.Controls.Add(image);

            HtmlGenericControl featuredListingContainer = new HtmlGenericControl("div");
            featuredListingContainer.Attributes.Add("class", "column-container2");

            HtmlGenericControl featuredListing = new HtmlGenericControl("div");
            featuredListing.Attributes.Add("class", "featured-listing");

            HtmlGenericControl title = new HtmlGenericControl("div");
            title.Attributes.Add("class", "column-title");
            title.InnerHtml = "Featured Listing";

            HtmlGenericControl listingTitle = new HtmlGenericControl("div");
            listingTitle.Attributes.Add("class", "featured-listing-title");
            listingTitle.InnerHtml = "<a href='/listing/" + ad.Id + "'>" + ad.Title + "</a>";

            HtmlGenericControl listingData = new HtmlGenericControl("div");
            listingData.Attributes.Add("class", "featured-listing-data");
            listingData.InnerHtml = "Posted " + Utility.GetRelativeTimeString(ad.Created);

            HtmlGenericControl listingDescription = new HtmlGenericControl("div");
            listingDescription.Attributes.Add("class", "featured-description");
            listingDescription.InnerHtml = ad.Description;

            featuredListing.Controls.Add(title);
            featuredListing.Controls.Add(listingTitle);
            featuredListing.Controls.Add(listingData);
            featuredListing.Controls.Add(listingDescription);

            featuredListingContainer.Controls.Add(imageContainer);
            featuredListingContainer.Controls.Add(featuredListing);

            Listings.Controls.Add(featuredListingContainer);
        }

        private void DisplayFilters(College school)
        {
            foreach (Filter filter in Filter.GetFilters())
            {
                SchoolFilters.InnerHtml += "<a href='school/" + school.ShortName + "/" + filter.Name + "'>" + filter.DisplayName + "</a>";
            }
        }

        private void DisplayRecentBlogPosts()
        {
            int counter = 0;
            foreach (BlogPost post in Database.GetBlogPosts())
            {
                HtmlGenericControl listing = new HtmlGenericControl("div");
                listing.Attributes.Add("class", "column-item");
                listing.InnerHtml = "<div class='date'>" + post.GetShortCreationDate() + "</div><a href='/blog/" + post.Id + "'>" + post.Title + "</a>";

                RecentBlogPosts.Controls.Add(listing);

                if (counter++ == 10)
                {
                    return;
                }
            }
        }

        private void DisplayRecentListings(College school)
        {
            RecentListingsTitle.InnerHtml = "Recent " + school.ShortName + " Listings";

            List<Advertisement> advertisements = Database.GetAds(school.Id, "", 1, Order.None);

            foreach (Advertisement ad in advertisements)
            {
                HtmlGenericControl listing = new HtmlGenericControl("div");
                listing.Attributes.Add("class", "column-item");
                listing.InnerHtml = "<div class='date'>" + ad.GetShortCreationDate() + "</div><a href='/listing/" + ad.Id + "'>" + ad.Title + "</a>";

                RecentListings.Controls.Add(listing);
            }
        }

        private void DisplayPopularListings(College school)
        {
            PopularListingsTitle.InnerHtml = "Popular " + school.ShortName + " Listings";

            List<Advertisement> advertisements = Database.GetAds(school.Id, "", 1, Order.Views);

            foreach (Advertisement ad in advertisements)
            {
                HtmlGenericControl listing = new HtmlGenericControl("div");
                listing.Attributes.Add("class", "column-item");
                listing.InnerHtml = "<div class='date'>" + ad.GetShortCreationDate() + "</div><a href='/listing/" + ad.Id + "'>" + ad.Title + "</a>";

                PopularListings.Controls.Add(listing);
            }
        }

        private void DisplayPopularBoostedListings(College school)
        {
            List<Advertisement> advertisements = Database.GetAds(school.Id, " AND boosted = 1", 1, Order.Views);

            foreach (Advertisement ad in advertisements)
            {
                HtmlGenericControl listing = new HtmlGenericControl("div");
                listing.Attributes.Add("class", "column-item");
                listing.InnerHtml = "<div class='date'>" + ad.GetShortCreationDate() + "</div><a href='/listing/" + ad.Id + "'>" + ad.Title + "</a>";

                PopularBoostedListings.Controls.Add(listing);
            }
        }

        private void SetCustomHeaderColor(College school)
        {
            SchoolContainer.Style.Add("background", school.Color + " url('../Images/Home/left_border.png') left center no-repeat;");
        }

        private void SetCustomBackground(College school)
        {
            HtmlGenericControl css = new HtmlGenericControl();
            css.TagName = "style";
            css.Attributes.Add("type", "text/css");

            string imageUrl = "/Images/SchoolHomepage/" + school.ShortName.ToLower() + ".png";

            css.InnerHtml = @".homepage{background-image: url(" + imageUrl + ");}";

            Page.Header.Controls.Add(css);
        }

        private College GetSchool()
        {
            string school = GetCookieData("School");

            try
            {
                return College.Get(int.Parse(school));
            }
            catch
            {
                return null;
            }
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
    }
}