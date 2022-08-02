using HotListing.API.Data;

namespace HotListing.API.Contracts
{
    public interface IHotelsRepository: IGenericRepository<Hotel>
    {
        Task<Hotel> GetHotelDetails(int id);
    }
}
