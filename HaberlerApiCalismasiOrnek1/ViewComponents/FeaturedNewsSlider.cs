using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaberlerApiCalismasiOrnek1.ViewComponents
{
    public class FeaturedNewsSlider : ViewComponent
    {
        private readonly ConnectDb _connectDb = new ConnectDb();

        public IViewComponentResult Invoke()
        {
            List<HaberContent> haberContents = _connectDb.HaberContent.FromSqlRaw("SELECT TOP 10 * FROM [Haberler].[dbo].[HaberContent] ORDER BY NEWID()").ToList();

            return View(haberContents);
        }
    }
}
