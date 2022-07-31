using HotListing.API.Models.Hotel;

namespace HotListing.API.Models.Country
{

    public class CountryDetailDto: BaseCountryDto
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        //public string ShortName { get; set; }
        public List<HotelDto> Hotels { get; set; }

    }

}
