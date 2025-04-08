using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_253503_Volodin.Domain.Entities
{
	public class Game
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
		public double Price { get; set; }
		public string? Image { get; set; }
		public string? ImageMimeType { get; set; }
	}
}

