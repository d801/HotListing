using AutoMapper;
using HotListing.API.Data;
using HotListing.API.Models.Country;
using HotListing.API.Models.Hotel;

namespace HotListing.API.Configurations
{
    //1-Inherit from profile
    //2-create constructor that mapps between our data types
    //3-CreateMap<Original Data Type , Dto data type>().ReverseMap() "reverse map means i can map in either directions"
   //4-we need to let our application knows that automapper exists and we need to have it as an injectable resource for our
   //controller or anywhere it needs to be used so we're going to define it in Program.cs
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDetailDto>().ReverseMap(); //CountryDto
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, GetHotelDto>().ReverseMap();
        }
    }
}
