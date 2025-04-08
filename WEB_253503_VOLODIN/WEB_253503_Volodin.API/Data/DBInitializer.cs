using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEB_253503_Volodin.Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using WEB_253503_Volodin.API.Data;

namespace WEB_253503_Volodin.API.Data
{
	public static class DbInitializer
	{
		public static async Task SeedData(WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

			await context.Database.MigrateAsync();


			if (context.Categories.Any() || context.Games.Any())
			{
				return;
			}

			var baseUrl = app.Configuration.GetValue<string>("AppSettings:BaseUrl");

			var categories = new Category[]
			{
				new Category { Name = "Стратегии", NormalizedName = "strategy" },
				new Category { Name = "Симуляторы", NormalizedName = "simulator" },
			};

			await context.Categories.AddRangeAsync(categories);
			await context.SaveChangesAsync();

			var games = new Game[]
			{
				new Game { Name = "Dota 2", Description = "Соревновательная MOBA-стратегия", Price = 0, Image = $"{baseUrl}/Images/dota.jpg", ImageMimeType = "image/jpeg", Category = categories.First(c => c.NormalizedName == "strategy") },
				new Game { Name = "Factorio", Description = "Симулятор фабрики.", Price = 14.99, Image = $"{baseUrl}/Images/factorio.jpg", ImageMimeType = "image/jpeg", Category = categories.First(c => c.NormalizedName == "simulator") },
				new Game { Name = "Fifa 24", Description = "Симулятор футбола", Price = 44.99, Image = $"{baseUrl}/Images/fifa24.jpg", ImageMimeType = "image/jpeg", Category = categories.First(c => c.NormalizedName == "simulator") },
				new Game { Name = "Farming simulator 23", Description = "Симулятор сельского хозяйства", Price = 34.99, Image = $"{baseUrl}/Images/fs23.jpg", ImageMimeType = "image/jpeg", Category = categories.First(c => c.NormalizedName == "simulator") },
				new Game { Name = "StarCraft 2", Description = "Стратегия в космическом будущем", Price = 0, Image = $"{baseUrl}/Images/starcraft2.jpg", ImageMimeType = "image/jpeg", Category = categories.First(c => c.NormalizedName == "strategy") },
			};

			await context.Games.AddRangeAsync(games);
			await context.SaveChangesAsync();
			Console.WriteLine("РАБОТАЕТ");
		}
	}
}
