using Microsoft.AspNetCore.Mvc;
using WEB_253503_Volodin.UI.Controllers;
using WEB_253503_Volodin.Domain.Entities;
using WEB_253503_Volodin.Domain.Models;
using WEB_253503_Volodin.UI.Services.CategoryService;

namespace WEB_253503_Volodin.UI.Services.GameService
{
	public class MemoryGameService : IGameService
	{
		private readonly IConfiguration _configuration;
		private List<Game> _games;
		private List<Category> _categories;
		private readonly ICategoryService _categoryService;

		public MemoryGameService(
			IConfiguration config,
			ICategoryService categoryService)
		{
			_configuration = config;
			_categories = categoryService.GetCategoryListAsync().Result.Data;
			SetupData();
		}



		private void SetupData()
		{
			_games = new List<Game>
			{
				new Game
				{
					Id = 1,
					Name = "Dota 2",
					Description = "Соревновательная MOBA-стратегия",
					Price = 0,
					Image = "Images/Dota.jpg",
					Category = _categories.Find(c => c.NormalizedName.Equals("strategy"))
				},
				new Game
				{
					Id = 2,
					Name = "Factorio",
					Description = "Симулятор фабрики",
					Price = 14.99,
					Image = "Images/Factorio.jpg",
					Category = _categories.Find(c => c.NormalizedName.Equals("strategy"))
				},
				new Game
				{
					Id = 3,
					Name = "Farming simulator 23",
					Description = "Симулятор сельского хозяйства",
					Price = 34.99,
					Image = "Images/Fs23.jpg",
					Category = _categories.Find(c => c.NormalizedName.Equals("simulator"))
				},
				new Game
				{
					Id = 4,
					Name = "Fifa 23",
					Description = "Симулятор футбола",
					Price = 44.99,
					Image = "Images/fifa24.jpg",
					Category = _categories.Find(c => c.NormalizedName.Equals("simulator"))
				},
				new Game
				{
					Id = 5,
					Name = "StarCraft 2",
					Description = "Стратегия в космическом будущем",
					Price = 0,
					Image = "Images/starcraft2.jpg",
					Category = _categories.Find(c => c.NormalizedName.Equals("strategy"))
				},
			};
		}
		public async Task<ResponseData<ListModel<Game>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
		{
			// Получаем размер страницы из конфигурации
			var itemsPerPage = _configuration.GetValue<int>("PageSettings:ItemsPerPage");

			var filteredItems = _games
				.Where(m => categoryNormalizedName == null || (m.Category != null && m.Category.NormalizedName.Equals(categoryNormalizedName)))
				.ToList();

			int totalItems = filteredItems.Count;
			int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

			var pagedItems = filteredItems
				.Skip((pageNo - 1) * itemsPerPage)
				.Take(itemsPerPage)
				.ToList();

			var result = new ListModel<Game>
			{
				Items = pagedItems,
				CurrentPage = pageNo,
				TotalPages = totalPages
			};

			return ResponseData<ListModel<Game>>.Success(result);
		}



		public Task<ResponseData<Game>> GetProductByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
		public Task DeleteProductAsync(int id)
		{
			throw new NotImplementedException();
		}
		public Task<ResponseData<Game>> CreateProductAsync(Game product, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}
		public Task UpdateProductAsync(int id, Game game, IFormFile? formFile)
		{
			throw new NotImplementedException();
		}
	}
}
