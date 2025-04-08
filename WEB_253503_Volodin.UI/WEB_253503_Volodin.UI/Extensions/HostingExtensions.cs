using WEB_253503_Volodin.UI.Services.CategoryService;
using WEB_253503_Volodin.UI.Services.GameService;

namespace WEB_253503_Volodin.UI.Extensions
{
	public static class HostingExtensions
	{
		public static void RegisterCustomServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
			builder.Services.AddScoped<IGameService, MemoryGameService>();
		}
	}
}
