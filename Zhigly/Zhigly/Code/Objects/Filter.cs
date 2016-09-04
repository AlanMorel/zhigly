using System;

namespace Zhigly.Code.Objects
{
    public class Filter
    {
        private static readonly Filter[] Filters = {
            new Filter(0, "All", "All", null, ""),
            new Filter(1, "Buying", "Buying", "Buying", " AND category = 0"),
            new Filter(2, "Selling", "Selling", "Selling", " AND category = 1"),
            new Filter(3, "Events", "Events", "Events", " AND category = 2"),
            new Filter(4, "Housing", "Housing", "Housing", " AND category = 3"),
            new Filter(5, "Opportunities", "Opportunities", "Opportunities", " AND category = 4"),
            new Filter(6, "Services", "Services", "Services", " AND category = 5"),
            new Filter(7, "Other", "Other", null, " AND category = 6"),
            new Filter(8, "January", "January", null, " AND month = december")
        };

        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Category Category { get; set;}
        public string Query { get; set; }

        public Filter(int Id, string Name, string DisplayName, string CategoryName, string Query)
        {
            this.Id = Id;
            this.Name = Name;
            this.DisplayName = DisplayName;
            this.Category = Category.Get(CategoryName);
            this.Query = Query;
        }

        public string GetQuery(Subcategory subcategory)
        {
            return Query + (subcategory == null ? "" : " AND subcategory = " + subcategory.Id);
        }

        public bool HasSubCategories()
        {
            return Category != null && Category.Subcategories.Length > 0;
        }

        public static Filter[] GetFilters()
        {
            return Filters;
        }

        public static Filter Get(int id)
        {
            return Array.Find(Filters, filter => filter.Id == id);
        }

        public static Filter Get(string name)
        {
            return Array.Find(Filters, filter => string.Equals(name, filter.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}