using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Schools : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (College school in College.GetColleges())
            {
                HtmlGenericControl schoolContainer = new HtmlGenericControl("div");
                schoolContainer.Attributes.Add("class", "school-container");

                HtmlGenericControl schoolLogoContainer = new HtmlGenericControl("div");
                schoolLogoContainer.Attributes.Add("class", "school-logo-container");

                Image schoolLogo = new Image();
                schoolLogo.CssClass = "school-logo";
                schoolLogo.ImageUrl = "/Images/SchoolLogo/" + school.ShortName + ".png";

                schoolLogoContainer.Controls.Add(schoolLogo);

                HtmlGenericControl content = new HtmlGenericControl("div");
                content.Attributes.Add("class", "content");

                HtmlGenericControl title = new HtmlGenericControl("div");
                title.Attributes.Add("class", "title");

                HyperLink titleLink = new HyperLink();
                titleLink.Text = school.Name;
                titleLink.NavigateUrl = "/School/" + school.ShortName;

                title.Controls.Add(titleLink);

                HtmlGenericControl description = new HtmlGenericControl("div");
                description.Attributes.Add("class", "description");
                description.InnerText = school.Description;

                HtmlGenericControl stats = new HtmlGenericControl("div");
                stats.Attributes.Add("class", "stats");

                HtmlGenericControl statsStats = new HtmlGenericControl("div");
                statsStats.Attributes.Add("class", "stats-stats");

                HtmlGenericControl listingsStat = new HtmlGenericControl("div");
                listingsStat.Attributes.Add("class", "stats-stat");
                listingsStat.InnerText = school.Listings.ToString();

                HtmlGenericControl usersStats = new HtmlGenericControl("div");
                usersStats.Attributes.Add("class", "stats-stat");
                usersStats.InnerText = school.Users.ToString();
                
                statsStats.Controls.Add(listingsStat);
                statsStats.Controls.Add(usersStats);

                HtmlGenericControl schoolStatsHeaders = new HtmlGenericControl("div");
                schoolStatsHeaders.Attributes.Add("class", "stats-headers");

                HtmlGenericControl listingsHeader = new HtmlGenericControl("div");
                listingsHeader.Attributes.Add("class", "stats-header");
                listingsHeader.InnerText = "Listings";

                HtmlGenericControl usersHeader = new HtmlGenericControl("div");
                usersHeader.Attributes.Add("class", "stats-header");
                usersHeader.InnerText = "Users";

                schoolStatsHeaders.Controls.Add(listingsHeader);
                schoolStatsHeaders.Controls.Add(usersHeader);

                stats.Controls.Add(statsStats);
                stats.Controls.Add(schoolStatsHeaders);

                content.Controls.Add(title);
                content.Controls.Add(description);
                content.Controls.Add(stats);

                schoolContainer.Controls.Add(schoolLogoContainer);
                schoolContainer.Controls.Add(content);

                SchoolContainers.Controls.Add(schoolContainer);
            }
        }
    }
}