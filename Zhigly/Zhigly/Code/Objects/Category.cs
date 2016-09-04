using System;

namespace Zhigly.Code.Objects
{
    public class Category
    {
        private static readonly Category[] Categories = {
            new Category(0, "Buying", "Buying",
                new Subcategory(0, "Book/Textbook", "Books and Textbooks"),
                new Subcategory(1, "Electronics", "Electronics"),
                new Subcategory(2, "Ticket", "Tickets"),
                new Subcategory(3, "Furniture", "Furniture"),
                new Subcategory(99, "Other", "Other")
            ),
            new Category(1, "Selling", "Selling",
                new Subcategory(0, "Book/Textbook", "Books and Textbooks"),
                new Subcategory(1, "Electronics", "Electronics"),
                new Subcategory(2, "Ticket", "Tickets"),
                new Subcategory(3, "Furniture", "Furniture"),
                new Subcategory(99, "Other", "Other")
            ),
            new Category(2, "Events", "Event",
                new Subcategory(0, "Party", "Parties"),
                new Subcategory(1, "Club Event", "Club Events"),
                new Subcategory(2, "Study Session", "Study Sessions"),
                new Subcategory(99, "Other", "Other")
            ),
            new Category(3, "Housing", "Housing",
                new Subcategory(0, "Dorm", "Dorms"),
                new Subcategory(1, "Apartment", "Apartments"),
                new Subcategory(2, "House", "Houses"),
                new Subcategory(3, "Roommate", "Roommates"),
                new Subcategory(99, "Other", "Other")
            ),
            new Category(4, "Opportunities", "Opportunity",
                new Subcategory(0, "Full-Time", "Full-Time"),
                new Subcategory(1, "Part-Time", "Part-Time"),
                new Subcategory(2, "Internship", "Internships"),
                new Subcategory(3, "Volunteer", "Volunteering"),
                new Subcategory(99, "Other", "Other")
            ),
            new Category(5, "Services", "Service",
                 new Subcategory(0, "Tutoring", "Tutoring"),
                 new Subcategory(1, "Lost and Found", "Lost and Found"),
                 new Subcategory(2, "Transportation", "Transportation"),
                 new Subcategory(99, "Other", "Other")
            ),
            new Category(6, "Other", "Other",
                 new Subcategory(0, "Other", "Other")
            ),
        };

        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Subcategory[] Subcategories { get; set; }

        public Category(int Id, string Name, string DisplayName, params Subcategory[] Subcategories)
        {
            this.Id = Id;
            this.Name = Name;
            this.DisplayName = DisplayName;
            this.Subcategories = Subcategories;
        }

        public Subcategory GetSubcategory(string name)
        {
            return Array.Find(Subcategories, subcategory => string.Equals(name, subcategory.GetUrlFriendlyName(), StringComparison.OrdinalIgnoreCase));
        }

        public Subcategory GetSubcategory(int id)
        {
            return Array.Find(Subcategories, subcategory => subcategory.Id == id);
        }

        public static Category[] GetCategories()
        {
            return Categories;
        }

        public static Category Get(int id)
        {
            return Array.Find(Categories, category => category.Id == id);
        }

        public static Category Get(string name)
        {
            return Array.Find(Categories, category => string.Equals(name, category.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}