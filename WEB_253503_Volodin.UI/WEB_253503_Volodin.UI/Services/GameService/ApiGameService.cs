using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WEB_253503_Volodin.Domain.Entities;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.IO;
using WEB_253503_Volodin.Domain.Models;

namespace WEB_253503_Volodin.UI.Services.GameService
{
	public class ApiGameService : IGameService
	{
		private readonly HttpClient _httpClient;

		public ApiGameService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<ResponseData<ListModel<Game>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
		{
			string url = $"games/categories/{categoryNormalizedName}?pageNo={pageNo}";
			Console.WriteLine(url);
			return await _httpClient.GetFromJsonAsync<ResponseData<ListModel<Game>>>(url);
		}

		public async Task<ResponseData<Game>> GetProductByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<ResponseData<Game>>($"games/{id}");
		}

		public async Task UpdateProductAsync(int id, Game product, IFormFile? formFile)
		{
			if (formFile != null)
			{
				using var form = new MultipartFormDataContent();
				using var fileStream = formFile.OpenReadStream();
				using var fileContent = new StreamContent(fileStream);
				fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(formFile.ContentType);
				form.Add(fileContent, "file", formFile.FileName);
				form.Add(new StringContent(id.ToString()), "id");
				form.Add(new StringContent(product.Name), "name");
				form.Add(new StringContent(product.Description), "description");
				form.Add(new StringContent(product.Price.ToString()), "price");
				form.Add(new StringContent(product.CategoryId.ToString()), "categoryId");

				var response = await _httpClient.PutAsync($"games/{id}", form);
				response.EnsureSuccessStatusCode();
			}
			else
			{
				var response = await _httpClient.PutAsJsonAsync($"games/{id}", product);
				response.EnsureSuccessStatusCode();
			}
		}

		public async Task DeleteProductAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"games/{id}");
			response.EnsureSuccessStatusCode();
		}

		public async Task<ResponseData<Game>> CreateProductAsync(Game product, IFormFile? formFile)
		{
			HttpResponseMessage response;

			if (formFile != null)
			{
				using var form = new MultipartFormDataContent();
				using var fileStream = formFile.OpenReadStream();
				using var fileContent = new StreamContent(fileStream);
				fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(formFile.ContentType);
				form.Add(fileContent, "file", formFile.FileName);
				form.Add(new StringContent(product.Name), "name");
				form.Add(new StringContent(product.Description), "description");
				form.Add(new StringContent(product.Price.ToString()), "price");
				form.Add(new StringContent(product.CategoryId.ToString()), "categoryId");

				response = await _httpClient.PostAsync("games", form);
			}
			else
			{
				response = await _httpClient.PostAsJsonAsync("games", product);
			}

			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<ResponseData<Game>>();
			}
			else
			{
				throw new HttpRequestException($"Error creating game: {response.ReasonPhrase}");
			}
		}
	}
}