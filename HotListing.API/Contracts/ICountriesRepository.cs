using HotListing.API.Data;

namespace HotListing.API.Contracts
{
    public interface ICountriesRepository : IGenericRepository<Country>
    {
        Task<Country> GetDetails(int id);

    }
}
