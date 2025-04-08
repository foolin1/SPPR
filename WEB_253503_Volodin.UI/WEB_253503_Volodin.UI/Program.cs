using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WEB_253503_Volodin.UI.Models;
using WEB_253503_Volodin.UI.Services.CategoryService;
using WEB_253503_Volodin.UI.Services.GameService;
using WEB_253503_Volodin.UI.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));

builder.Services.AddDbContext<GameContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("GameContext")));

builder.RegisterCustomServices();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();