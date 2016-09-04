using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zhigly.Code;
using Zhigly.Code.Objects;

namespace Zhigly
{
    public partial class Blog : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string blogPost = Page.RouteData.Values["post"] as string;
            
            int blogNumber;

            if (string.IsNullOrEmpty(blogPost) || !int.TryParse(blogPost, out blogNumber) || blogNumber < 1)
            {
                AddBlogPosts();
                return;
            }

            BlogPost post = Database.GetBlogPost(blogNumber);

            if (post == null)
            {
                Response.Redirect("~/Blog.aspx/");
                return;
            }

            DisplayBlogPost(post);
            AddSocialMedia(post.Id);

            Database.AddPostView(post.Id);
        }

        private void DisplayBlogPost(BlogPost post)
        {
            HtmlGenericControl postContainer = new HtmlGenericControl("div");
            postContainer.Attributes.Add("class", "blog-container");

            HtmlGenericControl imageContainer = new HtmlGenericControl("div");
            imageContainer.Attributes.Add("class", "image-container2");

            Image image = new Image
            {
                CssClass = "post-image2",
                ImageUrl = post.Image
            };

            imageContainer.Controls.Add(image);

            HtmlGenericControl textContainer = new HtmlGenericControl("div");
            textContainer.Attributes.Add("class", "text-container2");

            HtmlGenericControl titleContainer = new HtmlGenericControl("div");
            imageContainer.Attributes.Add("class", "title-container");

            HtmlGenericControl title = new HtmlGenericControl("div");
            title.Attributes.Add("class", "title2");
            title.InnerHtml = post.Title;
            
            HtmlGenericControl views = new HtmlGenericControl("div");
            views.Attributes.Add("class", "views");
            views.InnerHtml = "Views: " + (post.Views + 1);
            
            titleContainer.Controls.Add(title);
            titleContainer.Controls.Add(views);

            HtmlGenericControl info = new HtmlGenericControl("div");
            info.Attributes.Add("class", "info2");
            info.InnerHtml = "Posted on " + post.GetCreationDate() + " by " + post.User;

            HtmlGenericControl content = new HtmlGenericControl("div");
            content.Attributes.Add("class", "content2");
            content.InnerHtml = post.Content;

            textContainer.Controls.Add(titleContainer);
            textContainer.Controls.Add(info);
            textContainer.Controls.Add(content);

            postContainer.Controls.Add(imageContainer);
            postContainer.Controls.Add(textContainer);

            Container.Controls.Add(postContainer);

        }

        private void AddBlogPosts()
        {
            foreach (BlogPost post in Database.GetBlogPosts())
            {
                AddBlogPost(post);
            }
        }

        private void AddBlogPost(BlogPost post)
        {
            HtmlGenericControl postContainer = new HtmlGenericControl("div");
            postContainer.Attributes.Add("class", "post-container");

            HtmlGenericControl imageContainer = new HtmlGenericControl("div");
            imageContainer.Attributes.Add("class", "image-container");

            Image image = new Image
            {
                CssClass = "post-image",
                ImageUrl = post.Image
            };

            imageContainer.Controls.Add(image);

            HtmlGenericControl textContainer = new HtmlGenericControl("div");
            textContainer.Attributes.Add("class", "text-container");

            HtmlGenericControl title = new HtmlGenericControl("div");
            title.Attributes.Add("class", "title");
            title.InnerHtml = "<a href='/blog/" + post.Id + "'>" + post.Title + "</a>";

            HtmlGenericControl info = new HtmlGenericControl("div");
            info.Attributes.Add("class", "info");
            info.InnerHtml = "Posted on " + post.GetCreationDate() + " by " + post.User;

            HtmlGenericControl content = new HtmlGenericControl("div");
            content.Attributes.Add("class", "content");
            content.InnerHtml = post.Content;

            textContainer.Controls.Add(title);
            textContainer.Controls.Add(info);
            textContainer.Controls.Add(content);

            postContainer.Controls.Add(imageContainer);
            postContainer.Controls.Add(textContainer);

            Container.Controls.Add(postContainer);
        }

        private void AddSocialMedia(int id)
        {
            HtmlGenericControl socialMedia = new HtmlGenericControl("div");
            socialMedia.Attributes.Add("class", "social-media");
            socialMedia.InnerHtml = "<span class='share'>Share this post</span><hr />";

            HtmlGenericControl facebook = new HtmlGenericControl("div");
            facebook.Attributes.Add("class", "fb-like");
            facebook.Attributes.Add("data-href", "http://zhigly.com/blog/" + id);
            facebook.Attributes.Add("data-layout", "standard");
            facebook.Attributes.Add("data-action", "like");
            facebook.Attributes.Add("data-show-faces", "true");
            facebook.Attributes.Add("data-share", "true");
            
            HyperLink twitter = new HyperLink();
            twitter.NavigateUrl = "https://twitter.com/share";
            twitter.Attributes.Add("class", "twitter-share-button");
            twitter.Attributes.Add("data-text", "Check out this blog post on Zhigly!");
            twitter.Attributes.Add("data-size", "large");
            
            HtmlGenericControl plus = new HtmlGenericControl("div");
            plus.Attributes.Add("class", "g-plusone");
            plus.Attributes.Add("data-href", "http://zhigly.com/listing/" + id);
            plus.Attributes.Add("data-annotation", "inline");
            plus.Attributes.Add("data-width", "776");

            HtmlGenericControl comments = new HtmlGenericControl("div");
            comments.Attributes.Add("class", "fb-comments");
            comments.Attributes.Add("data-href", "http://zhigly.com/blog/" + id);
            comments.Attributes.Add("data-width", "776");
            comments.Attributes.Add("data-numposts", "5");

            socialMedia.Controls.Add(facebook);
            socialMedia.Controls.Add(twitter);
            socialMedia.Controls.Add(plus);

            Container.Controls.Add(socialMedia);
            Container.Controls.Add(comments);
        }
    }
}