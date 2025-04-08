using Microsoft.AspNetCore.Mvc;

namespace WEB_253503_Volodin.UI.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
