using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Controllers
{
    public class KategoriDetayController : Controller
    {
        readonly ConnectDb connectDb = new ConnectDb();

        [Route("kategoridetay")]
        public IActionResult Index(string categoryname)
        {
            List<HaberContent> newsList = (List<HaberContent>)connectDb.HaberContent.Where(x => x.Source == categoryname).OrderByDescending(x => x.Id).ToList();

            HaberContent source = connectDb.HaberContent.FirstOrDefault(x => x.Source == categoryname);

            KategoriDetayIndexViewModel model = new KategoriDetayIndexViewModel()
            {
                NewsList = newsList,
                OneNEws = source
            };
            return View(model);
        }

    }
}
