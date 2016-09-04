namespace Zhigly.Code.Objects
{
    public class Stats
    {
        public int Id { get; set; }
        public int Users { get; set; }
        public int Listings { get; set; }

        public Stats(int Id, int Users, int Listings)
        {
            this.Id = Id;
            this.Users = Users;
            this.Listings = Listings;
        }

    }
}