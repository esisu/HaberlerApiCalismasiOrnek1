using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HaberlerApiCalismasiOrnek1.ViewComponents
{
    public class HaberKanalKategorileri : ViewComponent
    {
        private readonly ConnectDb _connectDb = new ConnectDb();

        public IViewComponentResult Invoke(int page=1)
        {
            //IPagedList<HaberContent> haberContents = _connectDb.HaberContent.ToPagedList(page,3);
            //List<HaberContent> haberContents = _connectDb.HaberContent.FromSqlRaw("select distinct source from [Haberler].[dbo].[HaberContent] ").ToList();
            List<HaberContent> haberContents = _connectDb.HaberContent.Select(x=>new HaberContent()
            {
                Source = x.Source
            }).Distinct().ToList();
            
            return View(haberContents);
        }
    }
}
