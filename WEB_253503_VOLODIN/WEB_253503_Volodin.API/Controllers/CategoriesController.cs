using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB_253503_Volodin.Domain.Entities;
using WEB_253503_Volodin.Domain.Models;
using WEB_253503_Volodin.API.Services.CategoryService;

namespace WEB_253503_Volodin.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<ActionResult<ResponseData<List<Category>>>> GetCategories()
		{
			var result = await _categoryService.GetCategoryListAsync();
			return Ok(result);
		}
	}
}
