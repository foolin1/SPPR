using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Volodin.Domain.Entities;

public class GameContext : DbContext
{
	public GameContext(DbContextOptions<GameContext> options)
		: base(options)
	{
	}

	public DbSet<WEB_253503_Volodin.Domain.Entities.Game> Game { get; set; } = default!;
}
