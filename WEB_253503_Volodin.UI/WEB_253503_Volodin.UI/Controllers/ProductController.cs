using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEB_253503_Volodin.UI.Services.CategoryService;
using WEB_253503_Volodin.UI.Services.GameService;
using WEB_253503_Volodin.Domain.Entities;


namespace WEB_253503_Volodin.UI.Controllers
{
	public class ProductController : Controller
	{
		private readonly IGameService _gameService;
		private readonly ICategoryService _categoryService;
		private readonly IConfiguration _configuration;

		public ProductController(
			IGameService gameService,
			ICategoryService categoryService,
			IConfiguration configuration)
		{
			_gameService = gameService;
			_categoryService = categoryService;
			_configuration = configuration;
		}

		public async Task<IActionResult> Index(string? category, int pageNo = 1)
		{
			var categoriesResponse = await _categoryService.GetCategoryListAsync();
			if (!categoriesResponse.Successfull)
			{
				return NotFound("Categories could not be loaded.");
			}

			ViewBag.Categories = categoriesResponse.Data;

			var productResponse = await _gameService.GetProductListAsync(category, pageNo);
			if (!productResponse.Successfull)
			{
				return NotFound(productResponse.ErrorMessage);
			}

			var currentCategory = category;
			if (string.IsNullOrEmpty(currentCategory))
			{
				currentCategory = "Все Игры";
			}
			else
			{
				var selectedCategory = categoriesResponse.Data.FirstOrDefault(c => c.NormalizedName == currentCategory);
				currentCategory = selectedCategory?.Name ?? "Все Игры";
			}

			ViewBag.CurrentCategory = currentCategory;
			ViewBag.CurrentPage = pageNo;
			ViewBag.TotalPages = productResponse.Data.TotalPages;

			return View(productResponse.Data);
		}
	}
}