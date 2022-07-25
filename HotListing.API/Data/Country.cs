namespace HotListing.API.Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual IList<Hotel> Hotels { get; set; } // for one-to-many relation >> a country has many hotels 
    }
}