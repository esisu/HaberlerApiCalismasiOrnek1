using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Controllers
{
    public class HaberKanallariController : Controller
    {
        [Route("haberkanallari")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
