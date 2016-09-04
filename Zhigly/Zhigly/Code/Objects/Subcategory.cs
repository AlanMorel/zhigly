namespace Zhigly.Code.Objects
{
    public class Subcategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public Subcategory(int Id, string Name, string DisplayName)
        {
            this.Id = Id;
            this.Name = Name;
            this.DisplayName = DisplayName;
        }

        public string GetUrlFriendlyName()
        {
            return DisplayName.Replace(' ', '_').Replace('/', '_');
        }
    }
}