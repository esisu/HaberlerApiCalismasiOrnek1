using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaberlerApiCalismasiOrnek1.ViewComponents
{
    public class TwoBlocksNews : ViewComponent
    {
        private readonly ConnectDb _connectDb = new ConnectDb();

        public IViewComponentResult Invoke(int page = 1)
        {
            //IPagedList<HaberContent> haberContents = _connectDb.HaberContent.ToPagedList(page,3);
            List<HaberContent> haberContents = _connectDb.HaberContent.FromSqlRaw("SELECT TOP 10 * FROM HaberContent ORDER BY Id DESC").ToList();

            return View(haberContents);
        }
    }
}
