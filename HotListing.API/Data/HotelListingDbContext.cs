using Microsoft.EntityFrameworkCore;

//the database and application connection link is here
namespace HotListing.API.Data
{
    public class HotelListingDbContext : DbContext
    {
        //created a constructor that takes DbContext options
        //theses options are coming from the connection string options that we defined in Program.cs
        public HotelListingDbContext(DbContextOptions options) : base(options)  
        {
            //our tables and its configurations here
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //always call the base whenever you override
            base.OnModelCreating(modelBuilder);

            //i'll add here what makes me configure how the data will go in
            //"telling the EF when i build the model, this is what i want you to do"
            modelBuilder.Entity<Country>().HasData(
                new Country { Id=1,Name="Jamaica",ShortName="JM"},
                new Country { Id=2,Name="Bahamas",ShortName="BS"},
                new Country { Id=3,Name="Cayman Island",ShortName="CI"}
                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel {Id=1,Name="Sandals resort and spa",Address="Negril",CountryId=1,Rating=4.5 },
                new Hotel { Id = 2, Name = "Comfort suites", Address = "George town", CountryId = 3, Rating = 4.3 },
                new Hotel { Id = 3, Name = "Grand palldium", Address = "Nassua", CountryId = 2, Rating = 4 }
                );
        }

    }
}
