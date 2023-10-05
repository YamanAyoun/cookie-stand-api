using cookie_stand_api.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace cookie_stand_api.Data
{
	public class CookiesStandDbContext : DbContext
	{
		public CookiesStandDbContext(DbContextOptions<CookiesStandDbContext> options) : base(options)
		{
		}

		public DbSet<CookieStand> CookieStands { get; set; }
		public DbSet<HourlySales> HourSales { get; set; }
	}
}
