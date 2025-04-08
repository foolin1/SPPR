using Microsoft.AspNetCore.Mvc;

namespace WEB_253503_VOLODIN.UI.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
