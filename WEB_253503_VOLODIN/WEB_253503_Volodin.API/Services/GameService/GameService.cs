using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Volodin.API.Data;
using WEB_253503_Volodin.Domain.Models;
using WEB_253503_Volodin.Domain.Entities;

namespace WEB_253503_Volodin.API.Services.GameService
{
	public class GameService : IGameService
	{
		private readonly AppDbContext _context;
		private readonly int _maxPageSize = 20;

		public GameService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<ResponseData<ListModel<Game>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
		{
			if (pageSize > _maxPageSize)
				pageSize = _maxPageSize;

			var query = _context.Games.Include(m => m.Category).AsQueryable();
			var dataList = new ListModel<Game>();

			if (!string.IsNullOrEmpty(categoryNormalizedName))
			{
				query = query.Where(d => d.Category.NormalizedName.Equals(categoryNormalizedName));
			}

			var count = await query.CountAsync();
			if (count == 0)
			{
				return ResponseData<ListModel<Game>>.Success(dataList);
			}

			int totalPages = (int)Math.Ceiling(count / (double)pageSize);
			if (pageNo > totalPages)
				return ResponseData<ListModel<Game>>.Error("No such page");

			dataList.Items = await query
				.OrderBy(d => d.Id)
				.Skip((pageNo - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			dataList.CurrentPage = pageNo;
			dataList.TotalPages = totalPages;
			return ResponseData<ListModel<Game>>.Success(dataList);
		}

		public async Task<ResponseData<Game>> GetProductByIdAsync(int id)
		{
			var product = await _context.Games.FindAsync(id);
			if (product == null)
			{
				return ResponseData<Game>.Error("Product not found");
			}

			return ResponseData<Game>.Success(product);
		}

		public async Task UpdateProductAsync(int id, Game product)
		{
			if (id != product.Id)
			{
				throw new ArgumentException("Product ID mismatch");
			}

			_context.Entry(product).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteProductAsync(int id)
		{
			var product = await _context.Games.FindAsync(id);
			if (product == null)
			{
				throw new KeyNotFoundException("Product not found");
			}

			_context.Games.Remove(product);
			await _context.SaveChangesAsync();
		}

		public async Task<ResponseData<Game>> CreateProductAsync(Game product)
		{
			_context.Games.Add(product);
			await _context.SaveChangesAsync();
			return ResponseData<Game>.Success(product);
		}

		public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
		{
			if (formFile == null || formFile.Length == 0)
			{
				return ResponseData<string>.Error("No file uploaded.");
			}

			var imagePath = Path.Combine("wwwroot", "Images");
			if (!Directory.Exists(imagePath))
			{
				Directory.CreateDirectory(imagePath);
			}

			var fileName = $"{id}_{Path.GetFileName(formFile.FileName)}";
			var filePath = Path.Combine(imagePath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await formFile.CopyToAsync(stream);
			}

			var url = $"{Path.Combine("Images", fileName)}";
			return ResponseData<string>.Success(url);
		}
	}
}
