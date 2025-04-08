using WEB_253503_Volodin.Domain.Entities;
using WEB_253503_Volodin.Domain.Models;

namespace WEB_253503_Volodin.UI.Services.GameService
{
	public interface IGameService
	{
		public Task<ResponseData<ListModel<Game>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
		public Task<ResponseData<Game>> GetProductByIdAsync(int id);
		public Task UpdateProductAsync(int id, Game product, IFormFile? formFile);
		public Task DeleteProductAsync(int id);
		public Task<ResponseData<Game>> CreateProductAsync(Game product, IFormFile? formFile);
	}

}