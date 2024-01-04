using HaberlerApiCalismasiOrnek1.Areas.Admin.Models;
using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.ViewComponents.Dashboard
{
    public class _DashboardBoxNewsCountViewComponent : ViewComponent
    {
        static readonly ConnectDb connectDb = new();

        public IViewComponentResult Invoke()
        {
            List<DashboardViewModel> haberler = new List<DashboardViewModel>();

            var result = connectDb.HaberContent
                .GroupBy(h => new { h.Title })
                .Where(g => g.Count() > 1)
                .Select(g => new
                {
                    Count = g.Count(),
                    Title = g.Key.Title
                })
                .ToList();

            var totalNewsCount = connectDb.HaberContent.Count();

            int groupNewsCount = result.Count;

            DashboardViewModel model = new DashboardViewModel()
            {
                GroupNewsCount = groupNewsCount,
                TotalNewsCount = totalNewsCount
            };

            //foreach (var value in result)
            //{
            //    if (value.Count > 1)
            //    {
            //        haberler.Add(new DashboardViewModel()
            //        {
            //            //Title = value.Title,
            //            GroupNewsCount = value.Count,
            //            //Id = value.Id,
            //        });
            //    }
            //}

            return View(model);
        }
    }
}
