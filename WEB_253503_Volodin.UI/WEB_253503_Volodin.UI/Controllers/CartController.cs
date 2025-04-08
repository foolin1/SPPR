using Microsoft.AspNetCore.Mvc;

namespace WEB_253503_Volodin.UI.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
