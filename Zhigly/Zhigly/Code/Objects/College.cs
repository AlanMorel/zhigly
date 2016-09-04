using System;

namespace Zhigly.Code.Objects
{
    public class College
    {
        private static readonly College[] Colleges = {
            new College(0, "New York University", "NYU", "#57068C", "New York University is a private, nonsectarian American research university based in New York City. Founded in 1831, NYU is one of the largest private non-profit institutions of American higher education. NYU's main campus is located at Greenwich Village in Lower Manhattan with institutes and centers on the Upper East Side, academic buildings and dorms down on Wall Street, and the Brooklyn campus located at MetroTech Center in Downtown Brooklyn.",
                new int[] {56, 57},
                "nyu.edu"
                ),
            new College(1, "Columbia University", "Columbia", "#3c86cf", "Columbia University is a private Ivy League research university in Upper Manhattan, New York City. It was established in 1754 as King's College by royal charter of George II of Great Britain and is the oldest institution of higher learning in New York State as well as one of the country's nine colonial colleges. After the revolutionary war, King's College briefly became a state entity, and was renamed Columbia College in 1784.",
                new int[] { },
                "columbia.edu"
                )
        };

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int[] Featured { get; set; }
        public int Users { get; set; }
        public int Listings { get; set; }
        public string[] Emails { get; set; }

        public College(int Id, string Name, string ShortName, string Color, string Description, int[] Featured, params string[] Emails)
        {
            this.Id = Id;
            this.Name = Name;
            this.ShortName = ShortName;
            this.Color = Color;
            this.Description = Description;
            this.Featured = Featured;
            this.Emails = Emails;
        }

        public int GetFeaturedListing()
        {
            if (Featured.Length < 1)
            {
                return -1;
            }

            int index = new Random().Next(0, Featured.Length);

            return Featured[index];
        }

        public static College[] GetColleges()
        {
            return Colleges;
        }

        public static College Get(int id)
        {
            return Array.Find(Colleges, college => college.Id == id);
        }

        public static College Get(string name)
        {
            return Array.Find(Colleges, college => string.Equals(name, college.ShortName, StringComparison.OrdinalIgnoreCase));
        }
    }
}