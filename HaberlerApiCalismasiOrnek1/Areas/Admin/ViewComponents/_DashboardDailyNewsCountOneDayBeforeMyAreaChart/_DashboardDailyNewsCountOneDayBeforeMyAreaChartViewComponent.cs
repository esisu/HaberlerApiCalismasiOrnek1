using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.ViewComponents._DashboardDailyNewsCountOneDayBeforeMyAreaChart
{
    public class _DashboardDailyNewsCountOneDayBeforeMyAreaChartViewComponent : ViewComponent
    {

        static readonly ConnectDb connectDb = new();

        public IViewComponentResult Invoke()
        {
            DateTime simdikiTarih = DateTime.Now;
            // 10 gün öncesine kadar olan tarihleri döngüyle yazdır
            //for (int i = 0; i < 10; i++)
            //{
            //    DateTime tarih = simdikiTarih.AddDays(-i);
            //    Console.WriteLine(tarih.ToString("dd/MM/yyyy")); // Tarihi istediğiniz formatta yazdırabilirsiniz.
            //}

            string small10Month = "";
            string small10Day = "";

            List<DayNews> dayNews = new List<DayNews>();

            for (int i = 1; i <= 7; i++)
            {
                //int dailyNewsCountOneDayBefore = connectDb.HaberContent.FromSqlRaw($"select * from habercontent WHERE DATEFROMPARTS({DateTime.Now.Year},{DateTime.Now.Month},{DateTime.Now.Day - i})=DATEFROMPARTS(YEAR(NewsDate),MONTH(NewsDate),DAY(NewsDate))").Count();

                //TODO: Sayılar düzgün gelmiyor
                DateTime tarih = simdikiTarih.AddDays(-i);
                if (tarih.Month < 10)
                {
                    small10Month = $"0{tarih.Month}";
                }
                else
                {
                    small10Month = $"{tarih.Month}";
                }
                if (tarih.Day < 10)
                {
                    small10Day = $"0{tarih.Day}";
                }
                else
                {
                    small10Day = $"{tarih.Day}";
                }
                int dailyNewsCountOneDayBefore = connectDb.HaberContent.FromSqlRaw($"SELECT * FROM HaberContent WHERE NewsDate LIKE '%{tarih.Year}-{small10Month}-{small10Day}%'").Count();

                dayNews.Add(new DayNews() { Count = dailyNewsCountOneDayBefore, Day = simdikiTarih.AddDays(-i) });
            }
            return View(dayNews.OrderBy(x => x.Day).ToList());
        }
    }

    public class DayNews
    {
        public int Count { get; set; }

        public DateTime Day { get; set; }

    }

}
