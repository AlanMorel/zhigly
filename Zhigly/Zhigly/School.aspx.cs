using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class School : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string schoolName = Page.RouteData.Values["name"] as string;
            string filterName = Page.RouteData.Values["filter"] as string;

            string info1 = Page.RouteData.Values["info1"] as string;
            string info2 = Page.RouteData.Values["info2"] as string;

            if (string.IsNullOrEmpty(schoolName))
            {
                Response.Redirect("~/Schools.aspx");
                return;
            }

            College school = College.Get(schoolName);

            if (school == null)
            {
                Response.Redirect("~/Schools.aspx");
                return;
            }

            Filter filter = Filter.Get(filterName);

            if (filter == null)
            {
                filter = Filter.GetFilters()[0];
            }

            Subcategory subcategory = null;

            int page;

            int.TryParse(info1, out page);

            if (filter.HasSubCategories() && page < 1)
            {
                subcategory = filter.Category.GetSubcategory(info1);
                int.TryParse(info2, out page);
            }

            if (page < 1)
            {
                page = 1;
            }
            
            List<Advertisement> advertisements = Database.GetAds(school.Id, filter.GetQuery(subcategory), page, Order.Boosted);

            AddAds(advertisements);

            string subUrl = subcategory != null ? subcategory.Name + "/" : "";
            AddNavigation(advertisements, "~/School/" + school.ShortName + "/" + filter.Name + "/" + subUrl, page);

            Page.Title = "Zhigly - " + filter.Name + " listings at " + school.ShortName;
            Banner.ImageUrl = "~/Images/SchoolBanner/" + school.ShortName + ".png";

            LeftHeader.InnerText = school.Name;
            ListingsSection.InnerText = (subcategory != null? subcategory.Name : filter.Name) + " listings at " + school.Name;
            PageNumber.InnerText = "Page " + page;

            AddSubcategories(filter, subcategory, school);

            AddFilters(schoolName, filter);
            SetCustomHeaderColor(school);
        }

        private void AddSubcategories(Filter filter, Subcategory sub, College school)
        {
            if (!filter.HasSubCategories())
            {
                return;
            }

            Category category = filter.Category;

            HtmlGenericControl all = new HtmlGenericControl("div");
            all.InnerHtml = "<a href='/school/" + school.ShortName + "/" + filter.Name + "/'>All " + filter.Name + "</a>";
            all.Attributes.Add("class", "subcategory");

            if (sub == null)
            {
                all.Attributes.Add("class", "subcategory selected-subcategory");
            }

            Subcategories.Controls.Add(all);

            foreach (Subcategory subcategory in category.Subcategories)
            {
                HtmlGenericControl subcategoryDivider = new HtmlGenericControl("div");
                subcategoryDivider.InnerHtml = "<a href='/school/" + school.ShortName + "/" + filter.Name + "/" + subcategory.GetUrlFriendlyName() + "'>" + subcategory.DisplayName + "</a>";
                subcategoryDivider.Attributes.Add("class", "subcategory");

                if (subcategory.Equals(sub))
                {
                    subcategoryDivider.Attributes.Add("class", "subcategory selected-subcategory");
                }

                Subcategories.Controls.Add(subcategoryDivider);
            }
        }

        private void AddAds(List<Advertisement> advertisements)
        {

            if (advertisements == null || advertisements.Count == 0)
            {
                AddNoAdsMessage();
                return;
            }

            foreach (Advertisement ad in advertisements)
            {
                AddAdvertisement(ad);
            }
        }

        private static bool HasPrevious(int page)
        {
            return page > 1;
        }

        private static bool HasNext(List<Advertisement> advertisements)
        {
            return advertisements != null && advertisements.Count == Constants.ListingsPerPage;
        }

        private void AddNavigation(List<Advertisement> advertisements, string url, int page)
        {
            HtmlGenericControl navContainer = new HtmlGenericControl("div");
            navContainer.Attributes.Add("class", "nav-container");

            if (HasPrevious(page))
            {
                HyperLink prevLink = new HyperLink();
                prevLink.Text = "<< Prev Page";
                prevLink.Attributes.Add("class", "nav");
                prevLink.NavigateUrl = url + (page - 1);

                navContainer.Controls.Add(prevLink);
            }

            if (HasPrevious(page) && HasNext(advertisements))
            {
                HtmlGenericControl divider = new HtmlGenericControl("span");
                divider.InnerHtml = "|";

                navContainer.Controls.Add(divider);
            }

            if (HasNext(advertisements))
            {
                HyperLink nextLink = new HyperLink();
                nextLink.Text = "Next Page >>";
                nextLink.Attributes.Add("class", "nav");
                nextLink.NavigateUrl = url + (page + 1);

                navContainer.Controls.Add(nextLink);
            }

            if (navContainer.Controls.Count != 0)
            {
                RightSection.Controls.Add(navContainer);
            }

        }

        private void AddNoAdsMessage()
        {
            HtmlGenericControl outer = new HtmlGenericControl("div");
            outer.Attributes.Add("class", "empty");

            HtmlGenericControl message = new HtmlGenericControl("div");
            message.Attributes.Add("class", "title");
            message.InnerHtml = "<h2>There are no listings here.</h2>";
            outer.Controls.Add(message);

            HtmlGenericControl message2 = new HtmlGenericControl("div");
            message2.Attributes.Add("class", "description");
            message2.InnerHtml = "<h3>Please check back again later!</h3>";
            outer.Controls.Add(message2);

            RightSection.Controls.Add(outer);
        }

        private string GetCssClass(Advertisement ad, User user)
        {
            if (ad.Boosted)
            {
                return "ad boost";
            }

            if (user.IsAccountVerified())
            {
                return "ad verified-ad";
            }

            if (ad.Views >= Constants.PopularityThreshold)
            {
                return "ad popular-ad";
            }

            return "ad";
        }

        private void AddAdvertisement(Advertisement ad)
        {
            User user = Database.GetUser(ad.User);

            if (user == null)
            {
                return;
            }

            HtmlGenericControl outer = new HtmlGenericControl("div");
            outer.Attributes.Add("class", GetCssClass(ad, user));

            HtmlGenericControl title = new HtmlGenericControl("div");
            title.InnerHtml = "<h2><a href='/Listing/" + ad.Id + "'>" + ad.Title + "</a></h2>";
            outer.Controls.Add(title);

            HtmlGenericControl posted = new HtmlGenericControl("div");
            posted.Attributes.Add("class", "created");

            if (ad.Anonymous)
            {
                posted.InnerHtml = "<p>Posted " + Utility.GetRelativeTimeString(ad.Created);
            }
            else
            {
                posted.InnerHtml = "<p>Posted " + Utility.GetRelativeTimeString(ad.Created) + " by " + user.GetDisplayName();
            }

            outer.Controls.Add(posted);

            HtmlGenericControl description = new HtmlGenericControl("div");
            description.Attributes.Add("class", "description");
            description.InnerHtml = "<h3>" + ad.Description + "</h3>";
            outer.Controls.Add(description);

            HtmlGenericControl icons = new HtmlGenericControl("div");
            icons.Attributes.Add("class", "icons");

            if (ad.Boosted)
            {
                HtmlGenericControl verified = new HtmlGenericControl("span");
                verified.Attributes.Add("class", "boosted");
                verified.Attributes.Add("title", "This is a boosted listing");

                icons.Controls.Add(verified);
            }

            if (user.IsAccountVerified())
            {
                HtmlGenericControl verified = new HtmlGenericControl("span");
                verified.Attributes.Add("class", "verified");
                verified.Attributes.Add("title", ad.Anonymous ? "This user is verified." : user.GetDisplayName() + " is a verified user.");

                icons.Controls.Add(verified);
            }

            if (ad.Views >= Constants.PopularityThreshold)
            {
                HtmlGenericControl popular = new HtmlGenericControl("span");
                popular.Attributes.Add("class", "popular");
                popular.Attributes.Add("title", "This listing is really popular.");

                icons.Controls.Add(popular);
            }

            foreach (string url in ad.Images)
            {
                HyperLink camera = new HyperLink();
                camera.Attributes.Add("class", "camera preview");
                camera.Attributes.Add("href", url);

                icons.Controls.Add(camera);
            }

            outer.Controls.Add(icons);
            RightSection.Controls.Add(outer);
        }

        private void AddFilters(string schoolName, Filter current)
        {
            foreach (Filter filter in Filter.GetFilters())
            {
                HyperLink hyperlink = new HyperLink();
                hyperlink.Text = filter.Name;
                hyperlink.NavigateUrl = "~/School/" + schoolName + "/" + filter.Name;

                if (filter.Equals(current))
                {
                    hyperlink.Attributes.Add("class", "selected");
                }

                LeftSection.Controls.Add(hyperlink);
            }
        }

        private void SetCustomHeaderColor(College school)
        {
            Style style = new Style();
            style.BackColor = ColorTranslator.FromHtml(school.Color);

            Page.Header.StyleSheet.CreateStyleRule(style, null, ".header");
        }
    }
}