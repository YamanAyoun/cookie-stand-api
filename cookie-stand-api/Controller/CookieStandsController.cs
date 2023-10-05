using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cookie_stand_api.Data;
using cookie_stand_api.Models.Entites;
using Microsoft.AspNetCore.Mvc.Rendering;
using cookie_stand_api.Models.DTOs;

namespace cookie_stand_api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CookieStandsController : ControllerBase
    {
        private readonly CookiesStandDbContext _context;

        public CookieStandsController(CookiesStandDbContext context)
        {
            _context = context;
        }

        // GET: api/CookieStands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CookieStand>>> GetCookieStands()
        {
			var cookieStands = await _context.CookieStands.ToListAsync();
			return cookieStands;
		}

		// GET: api/CookieStands/5
		[HttpGet("{id}")]
        public async Task<ActionResult<CookieStand>> GetCookieStand(int id)
        {
			var cookieStand = await _context.CookieStands.FindAsync(id);

			if (cookieStand == null)
			{
				return NotFound();
			}

			return cookieStand;
		}

		// PUT: api/CookieStands/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCookieStand(int id, CookiePostDto updateStand)

		{
			if (id != updateStand.Id)
			{
				return BadRequest("ID mismatch"); // Return a bad request if the provided ID doesn't match the object ID
			}

			// Find the existing cookie stand by ID in the database
			var existingCookieStand = await _context.CookieStands.FindAsync(id);

			if (existingCookieStand == null)
			{
				return NotFound(); // Return 404 if the object doesn't exist
			}

			// Update the properties of the existing cookie stand with the values from the updateStand DTO
			existingCookieStand.Location = updateStand.Location;
			existingCookieStand.Description = updateStand.Description;
			existingCookieStand.minimum_customers_per_hour = updateStand.Minimum_Customers_Per_Hour;
			existingCookieStand.maximum_customers_per_hour = updateStand.Maximum_Customers_Per_Hour;
			existingCookieStand.average_cookies_per_sale = updateStand.Average_Cookies_Per_Sale;
			existingCookieStand.Owner = updateStand.Owner;

			// Save changes to the database
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// POST: api/CookieStands
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<CookieStand>> PostCookieStand(CookieStand cookieStand)
        {
			_context.CookieStands.Add(cookieStand);
			await _context.SaveChangesAsync(); 
			return CreatedAtAction("GetCookieStand", new { id = cookieStand.Id }, cookieStand);
		}

        // DELETE: api/CookieStands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCookieStand(int id)
        {
			var cookieStand = await _context.CookieStands.FindAsync(id);

			if (cookieStand == null)
			{
				return NotFound();
			}

			_context.CookieStands.Remove(cookieStand);
			await _context.SaveChangesAsync();

			return NoContent();
		}
    }
}
