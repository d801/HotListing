using HotListing.API.Contracts;
using HotListing.API.Data;
using Microsoft.EntityFrameworkCore;

namespace HotListing.API.Repository
{
    //countries repository will be inheriting from both
    public class CountriesRepository: GenericRepository<Country>, ICountriesRepository //now that we've our specific repository creator, we need to register it in the program.cs 
    {
        private readonly HotelListingDbContext _context;
        //we need to initialize our generator constructor that'll initialize our DB context
        public CountriesRepository(HotelListingDbContext context):base (context)
        {
            this._context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.Countries
                .Include(x => x.Hotels)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
