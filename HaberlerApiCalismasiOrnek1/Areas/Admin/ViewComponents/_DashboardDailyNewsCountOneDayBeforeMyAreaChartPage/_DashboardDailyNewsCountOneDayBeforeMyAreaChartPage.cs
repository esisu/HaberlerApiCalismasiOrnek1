using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.ViewComponents._DashboardDailyNewsCountOneDayBeforeMyAreaChartPage
{
    public class _DashboardDailyNewsCountOneDayBeforeMyAreaChartPage : ViewComponent
    {
        static readonly ConnectDb connectDb = new();

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
