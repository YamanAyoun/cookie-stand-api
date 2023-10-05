using cookie_stand_api.Models.DTOs;
using cookie_stand_api.Models.Entites;

namespace cookie_stand_api.Services.Interfaces
{
	public interface ICookieStand
	{
		Task<List<CookieStand>> GetAllCookieStand();

		Task<CookieStand> GetCookieStandById(int id);

		Task<CookieStand> CreateCookieStand(CookiePostDto stand);

		Task<CookieStand> UpdateCookieStand(int id, CookiePostDto updateStand);

		Task<CookieStand> Delete(int id);
	}
}
