using Microsoft.EntityFrameworkCore;
using WEB_253503_Volodin.Domain.Entities;

namespace WEB_253503_Volodin.API.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Game> Games { get; set; }
	}
}
