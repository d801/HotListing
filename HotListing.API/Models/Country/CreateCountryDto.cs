using System.ComponentModel.DataAnnotations;

namespace HotListing.API.Models.Country
{
    //responsible for creating requests for country
    public class CreateCountryDto : BaseCountryDto
    {
        //[Required]
        //public string Name { get; set; }
        //public string ShortName { get; set; }
    }

    //responsible for the updating request
    public class UpdateCountryDto : BaseCountryDto
    {
        public int Id { get; set; }
    }
}
