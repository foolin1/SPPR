using WEB_253503_Volodin.Domain.Entities;
using WEB_253503_Volodin.Domain.Models;

namespace WEB_253503_Volodin.API.Services.CategoryService
{
	public interface ICategoryService
	{
		Task<ResponseData<List<Category>>> GetCategoryListAsync();
	}
}
