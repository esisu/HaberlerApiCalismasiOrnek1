using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Controllers
{
    public class HaberKanallariController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
