using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.ViewComponents
{
    public class Footer : ViewComponent
    {

        private readonly ConnectDb _connectDb = new ConnectDb();

        public IViewComponentResult Invoke()
        {
            List<HaberContent> haberContents = _connectDb.HaberContent.Select(x => new HaberContent()
            {
                Source = x.Source
            }).Distinct().ToList();

            List<HaberContent> lasthaberContents = _connectDb.HaberContent.Take(3).OrderByDescending(x => x.Id).ToList();

            FooterViewModel model = new FooterViewModel()
            {
                NewsSourceList = haberContents,
                Last3News= lasthaberContents
            };

            return View(model);
        }

    }
}
