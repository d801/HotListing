using System.ComponentModel.DataAnnotations.Schema;

namespace HotListing.API.Data
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; } //making this a foregin key to the country table, is what made us write the next property as a reference to the table
        public Country Country { get; set; }

    }
}
