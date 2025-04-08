using WEB_253503_Volodin.Domain.Entities;
using WEB_253503_Volodin.Domain.Models;

namespace WEB_253503_Volodin.UI.Services.CategoryService
{
	public interface ICategoryService
	{
		public Task<ResponseData<List<Category>>> GetCategoryListAsync();
	}
}