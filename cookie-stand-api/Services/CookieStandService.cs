using cookie_stand_api.Data;
using cookie_stand_api.Models.DTOs;
using cookie_stand_api.Models.Entites;
using cookie_stand_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace cookie_stand_api.Services
{
	public class CookieStandService : ICookieStand
	{
		private readonly CookiesStandDbContext _context;

		public CookieStandService(CookiesStandDbContext context)
		{
			_context = context;
		}

		public async Task<CookieStand> CreateCookieStand(CookiePostDto stand)
		{
			var cookieStand = new CookieStand
			{
				Location = stand.Location,
				Description = stand.Description,
				minimum_customers_per_hour = stand.Minimum_Customers_Per_Hour,
				maximum_customers_per_hour = stand.Maximum_Customers_Per_Hour,
				average_cookies_per_sale = stand.Average_Cookies_Per_Sale,
				Owner = stand.Owner
			};

			_context.CookieStands.Add(cookieStand);
			await _context.SaveChangesAsync();

			return cookieStand;
		}


		public async Task<CookieStand> Delete(int id)
		{
			var cookieStand = await _context.CookieStands.FindAsync(id);

			if (cookieStand == null)
			{
				return null;
			}

			_context.CookieStands.Remove(cookieStand);
			await _context.SaveChangesAsync();

			return cookieStand;
		}


		public async Task<List<CookieStand>> GetAllCookieStand()
		{
			return await _context.CookieStands.ToListAsync();
		}


		public async Task<CookieStand> GetCookieStandById(int id)
		{
			return await _context.CookieStands.FindAsync(id);
		}


		public async Task<CookieStand> UpdateCookieStand(int id, CookiePostDto updateStand)
		{
			var cookieStand = await _context.CookieStands.FindAsync(id);

			if (cookieStand == null)
			{
				return null;
			}

			cookieStand.Location = updateStand.Location;
			cookieStand.Description = updateStand.Description;
			cookieStand.minimum_customers_per_hour = updateStand.Minimum_Customers_Per_Hour;
			cookieStand.maximum_customers_per_hour = updateStand.Maximum_Customers_Per_Hour;
			cookieStand.average_cookies_per_sale = updateStand.Average_Cookies_Per_Sale;
			cookieStand.Owner = updateStand.Owner;

			await _context.SaveChangesAsync();

			return cookieStand;
		}

	}
}
