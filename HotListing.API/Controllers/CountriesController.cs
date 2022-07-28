using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotListing.API.Data;

namespace HotListing.API.Controllers
{
    //In ggeneral, the controller is the one who's going to recieve the request and process it and then send a response
    //and that's basically all APIs do
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly HotelListingDbContext _context; //initiakize private field of what we injects

        //Injecting our DB context to controller and that's because we registered our db context as a service in program.cs
        //it can be injected anywhere in the application
        //This saves us the trouble of instantiating a DB context every single ttime we want to interact with the database.
        //whatever the interaction with the database finished, it kills the instance in the background.
        //saves us memory time and effeciency
        public CountriesController(HotelListingDbContext context) //ctor
        {
            _context = context;
        }

        // GET: api/Countries   this is the address (endpoint)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
          if (_context.Countries == null)
          {
              return NotFound();
          }
            return await _context.Countries.ToListAsync();
            //above return line can be replaced with explicit 200 response OK();
            //var Countries = await _context.Countries.ToListAsync();
            //return Ok(Countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
          if (_context.Countries == null)
          {
              return NotFound();
          }
            var country = await _context.Countries.FindAsync(id); //find with the primary key value

            if (country == null)
            {
                return NotFound(); //404
            }

            return country;
            //can be replaced above line with return Ok(country);
        }

        // PUT: api/Countries/5
        //the HTTPPUT is for the update operation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();// I can add custom message here  return BadRequest("Invalid record here");
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
          if (_context.Countries == null)
          {
              return Problem("Entity set 'HotelListingDbContext.Countries'  is null.");
          }
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent(); //that's a response that means,
                                //I did the operation successfully and i really don't have anything to show
        }

        private bool CountryExists(int id)
        {
            return (_context.Countries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
