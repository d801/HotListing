using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotListing.API.Data;
using HotListing.API.Models.Country;
using AutoMapper;
using HotListing.API.Contracts;

namespace HotListing.API.Controllers
{
    //In ggeneral, the controller is the one who's going to recieve the request and process it and then send a response
    //and that's basically all APIs do
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
       // private readonly HotelListingDbContext _context; //initiakize private field of what we injects
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository; //the context got replaced with ICountries repositories because the repo will do the work with db on our behalf and the controller doesn't need to know the INs and OUTs of the query being run

        //Injecting our DB context to controller and that's because we registered our db context as a service in program.cs
        //it can be injected anywhere in the application
        //This saves us the trouble of instantiating a DB context every single ttime we want to interact with the database.
        //whatever the interaction with the database finished, it kills the instance in the background.
        //saves us memory time and effeciency
        public CountriesController(IMapper mapper , ICountriesRepository countriesRepository)//HotelListingDbContext context ,  //ctor
        {
            //_context = context;
            _mapper = mapper;
            _countriesRepository = countriesRepository; 
        }

        // GET: api/Countries   this is the address (endpoint)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {

            //var Countries = await _context.Countries.ToListAsync();
            var Countries = await _countriesRepository.GetAllAsync();
            var TheCountryList = _mapper.Map<List<GetCountryDto>>(Countries);
            return Ok(TheCountryList);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDetailDto>> GetCountry(int id)
        {
            //var country = await _context.Countries.Include(p=>p.Hotels).FirstOrDefaultAsync(x=>x.Id == id); //find with the primary key value

            var country = await _countriesRepository.GetDetails(id);

            var CountryDetail = _mapper.Map<CountryDetailDto>(country);

            if (country == null)
            {
                return NotFound(); //404
            }

            //return country;
            //can be replaced above line with return Ok(country);
            return Ok(CountryDetail);
        }

        // PUT: api/Countries/5
        //the HTTPPUT is for the update operation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754 //overposting means preventing the use from submitting data we don't want or need to be changed that's why we did the Dtos
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("This is invaild ID :(");// I can add custom message here  return BadRequest("Invalid record here");
            }

            //_context.Entry(country).State = EntityState.Modified;
            //var country = await _context.Countries.FindAsync(id);
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            _mapper.Map(updateCountryDto,country);
            try
            {
                //await _context.SaveChangesAsync();
                await _countriesRepository.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto CreateCountryDto)
        {
            //var countryOld = new Country
            //{
            //    Name = CreateCountryDto.Name,
            //    ShortName = CreateCountryDto.ShortName,
            //};

            //after adding the automapper
            var country = _mapper.Map<Country>(CreateCountryDto);
            //_context.Countries.Add(country);
            //await _context.SaveChangesAsync();
            await _countriesRepository.AddAsync(country);
            

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //var country = await _context.Countries.FindAsync(id);
            var country = await _countriesRepository.GetAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            //_context.Countries.Remove(country);            
            //await _context.SaveChangesAsync();
            await _countriesRepository.DeleteAsync(id);

            return NoContent(); //that's a response that means,
                                //I did the operation successfully and i really don't have anything to show
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countriesRepository.Exists(id);
            //return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
