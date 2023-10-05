using cookie_stand_api.Data;
using cookie_stand_api.Services;
using cookie_stand_api.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;

namespace cookie_stand_api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();
			string connString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services
				.AddDbContext<CookiesStandDbContext>
				(opions => opions.UseSqlServer(connString));
			builder.Services.AddTransient<ICookieStand, CookieStandService>();

			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
				{
					Title = "Cookie_Stand_Api",
					Version = "v1",
				});
			});
			var app = builder.Build();

			app.UseSwagger(options =>
			{
				options.RouteTemplate = "swagger/{documentName}/swagger.json";
			});

			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Cookie_Stand_Api");
				options.RoutePrefix = "swagger";
			});

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.MapGet("/", () => "Hello World!");

			app.Run();
		}
	}
}