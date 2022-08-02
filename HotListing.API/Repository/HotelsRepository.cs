using HotListing.API.Contracts;
using HotListing.API.Data;

namespace HotListing.API.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        private readonly HotelListingDbContext _context;
        public HotelsRepository(HotelListingDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<Hotel> GetHotelDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
