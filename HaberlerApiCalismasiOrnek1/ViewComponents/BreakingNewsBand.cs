using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaberlerApiCalismasiOrnek1.ViewComponents
{
    public class BreakingNewsBand : ViewComponent
    {
        private readonly ConnectDb _connectDb = new ConnectDb();

        public IViewComponentResult Invoke()
        {
            List<HaberContent> haberContents = _connectDb.HaberContent.FromSqlRaw("SELECT TOP 2 * FROM HaberContent ORDER BY Id DESC").ToList();

            return View(haberContents);
        }
    }
}
