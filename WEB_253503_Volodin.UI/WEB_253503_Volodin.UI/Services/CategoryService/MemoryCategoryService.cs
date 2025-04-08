using WEB_253503_Volodin.Domain.Entities;
using WEB_253503_Volodin.Domain.Models;
using WEB_253503_Volodin.UI.Services.CategoryService;

namespace WEB_253503_Volodin.UI.Services.CategoryService
{
	public class MemoryCategoryService : ICategoryService
	{
		public Task<ResponseData<List<Category>>> GetCategoryListAsync()
		{
			var categories = new List<Category>
			{
				new Category
				{
					Id = 1,
					Name = "Стратегии",
					NormalizedName = "strategy"
				},
				new Category
				{
					Id = 2,
					Name = "Симуляторы",
					NormalizedName = "simulator"
				}
			};
			var result = new ResponseData<List<Category>>();
			result.Data = categories;
			return Task.FromResult(result);
		}
	}
}
