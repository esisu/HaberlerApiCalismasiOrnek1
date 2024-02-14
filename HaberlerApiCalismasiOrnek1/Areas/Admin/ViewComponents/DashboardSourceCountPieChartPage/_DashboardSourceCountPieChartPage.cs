using HaberlerApiCalismasiOrnek1.Areas.Admin.Models;
using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.ViewComponents._DashboardSourceCountPieChartPage
{
    public class _DashboardSourceCountPieChartPage : ViewComponent
    {
        static readonly ConnectDb connectDb = new();

        public IViewComponentResult Invoke()
        {
            List<SourceAndCountViewModel> haberler = new List<SourceAndCountViewModel>();

            var query = connectDb.HaberContent
                .GroupBy(h => new { h.Source })
                .Select(g => new
                {
                    //Id = g.Key.Id,
                    Title = g.Key.Source,
                    Count = g.Count()
                });

            var results = query.ToList();

            Random rand = new Random();

            foreach (var value in results.Where(x => x.Count > 50).OrderByDescending(x => x.Count).Take(3))
            {
                haberler.Add(new SourceAndCountViewModel()
                {
                    Source = value.Title.ToUpper(),
                    SourceCount = value.Count,
                    //SourceColor = String.Format("#{0:X6}", rand.Next(0x1000000)), // = "#A197B9",
                    //SourceBorderColor = String.Format("#{0:X6}", rand.Next(0x1000000)),
                    //SourceHoverColor = String.Format("#{0:X6}", rand.Next(0x1000000))
                });
            }

            ViewBag.Data = JsonConvert.SerializeObject(haberler);

            return View(haberler);

            //TODO:Tümünü göre yapılacak. Haber kaynakları ve haber sayısı gösterilecek detay sayfada
        }
    }
}
