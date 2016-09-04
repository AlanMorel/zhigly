using System;

namespace Zhigly.Code.Objects
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int Views { get; set; }
        public DateTime Created { get; set; }

        public string GetCreationDate()
        {
            return Created.ToString("MMMM dd").Replace(" 0", " ");
        }

        public string GetShortCreationDate()
        {
            return Created.ToString("MMM dd").Replace(" 0", " ");
        }
    }
}