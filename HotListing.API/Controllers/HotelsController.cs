using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotListing.API.Data;
using HotListing.API.Contracts;
using AutoMapper;
using HotListing.API.Models.Hotel;

namespace HotListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelsController( IMapper mapper , IHotelsRepository hotelsRepository)
        {
            _mapper = mapper;   
            _hotelRepository = hotelsRepository;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            var Hotels = await _hotelRepository.GetAllAsync();
            var TheHotelList = _mapper.Map<List<GetHotelDto>>(Hotels);
            return Ok(TheHotelList);

        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetAsync(id);
            var HotelDetail = _mapper.Map<GetHotelDto>(hotel);
            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(HotelDetail);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id,HotelDto hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }
            //get the hotel
            var GetHotel = await _hotelRepository.GetAsync(id);
            if (GetHotel == null) 
            {
                return NotFound();
            }
            var HotelDetail = _mapper.Map(hotelDto, GetHotel);
            try
            {
                
                await _hotelRepository.UpdateAsync(HotelDetail);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDto hotelDto)
        {
            var TheHotel = _mapper.Map<Hotel>(hotelDto);
            await _hotelRepository.AddAsync(TheHotel);

            return CreatedAtAction("GetHotel", new { id = TheHotel.Id }, hotelDto);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotelRepository.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotelRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.Exists(id);
        }
    }
}
