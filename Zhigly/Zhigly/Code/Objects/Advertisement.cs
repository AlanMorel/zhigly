using System;
using System.Collections.Generic;

namespace Zhigly.Code.Objects
{
    public class Advertisement
    {
        public int Id { get; set; }
        public int School { get; set; }
        public int Category { get; set; }
        public int Subcategory { get; set; }
        public int User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public int Views { get; set; }
        public bool Boosted { get; set; }
        public bool Deleted { get; set; }
        public bool Anonymous { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }

        public bool HasAnImage()
        {
            return Images.Count > 0;
        }

        public string GetRandomImage()
        {
            if (Images.Count < 1)
            {
                return string.Empty;
            }

            Random random = new Random();
            int index = random.Next(0, Images.Count);

            return Images[index];
        }

        public void AddImages(string images)
        {
            if (string.IsNullOrEmpty(images))
            {
                Images = new List<string>();
            }
            else
            {
                Images = new List<string>(images.Split(','));
            }
        }

        public bool IsExpired()
        {
            return Expiration < DateTime.Now;
        }

        public string GetCreationDate()
        {
            return Created.ToString("MMMM dd").Replace(" 0", " ");
        }

        public string GetShortCreationDate()
        {
            return Created.ToString("MMM dd").Replace(" 0", " ");
        }

        public string GetExpirationDate()
        {
            return Expiration.ToString("MMMM dd").Replace(" 0", " ");
        }
    }
}