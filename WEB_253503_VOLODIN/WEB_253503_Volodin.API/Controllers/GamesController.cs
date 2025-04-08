using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using WEB_253503_Volodin.Domain.Models;
using WEB_253503_Volodin.API.Services.GameService;
using WEB_253503_Volodin.Domain.Entities;

namespace WEB_253503_Volodin.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GamesController : ControllerBase
	{
		private readonly IGameService _gameService;

		public GamesController(IGameService gameService)
		{
			_gameService = gameService;
		}

		[HttpGet("categories/{categoryName?}")]
		public async Task<ActionResult<ResponseData<List<Game>>>> GetGames(string? categoryName, int pageNo = 1, int pageSize = 3)
		{
			var result = await _gameService.GetProductListAsync(categoryName, pageNo, pageSize);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ResponseData<Game>>> GetGame(int id)
		{
			var result = await _gameService.GetProductByIdAsync(id);
			if (!result.Successfull || result.Data == null)
			{
				return NotFound(result.ErrorMessage);
			}
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> PutGame(int id, Game game)
		{
			if (id != game.Id)
			{
				return BadRequest("Game ID mismatch.");
			}

			await _gameService.UpdateProductAsync(id, game);
			return NoContent();
		}

		[HttpPost]
		public async Task<ActionResult<ResponseData<Game>>> PostGame(Game game)
		{
			var result = await _gameService.CreateProductAsync(game);
			return CreatedAtAction(nameof(GetGame), new { id = result.Data.Id }, result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGame(int id)
		{
			await _gameService.DeleteProductAsync(id);
			return NoContent();
		}
	}
}