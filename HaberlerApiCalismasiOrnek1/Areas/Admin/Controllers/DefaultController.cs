using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DefaultController : Controller
    {
        [Route("default")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
