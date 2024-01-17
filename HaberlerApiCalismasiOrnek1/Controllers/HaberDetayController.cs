using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Controllers
{
    public class HaberDetayController : Controller
    {
        readonly ConnectDb connectDb = new ConnectDb();

        [Route("haberdetay/{id}")]
        public IActionResult Index(int id)
        {
            HaberContent detailNews = connectDb.HaberContent.Find(id);

            HaberDetayViewModel model = new HaberDetayViewModel()
            {
                HaberContent = detailNews
            };

            return View(model);
        }
    }
}
