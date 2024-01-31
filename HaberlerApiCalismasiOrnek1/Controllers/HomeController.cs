using System.Globalization;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestRequest = RestSharp.RestRequest;
using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using System.Net;
using System.Net.Mail;
using Hangfire;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Sentry;

namespace HaberlerApiCalismasiOrnek1.Controllers
{
    public class HomeController : Controller
    {
        static readonly ConnectDb connectDb = new();
        public IHub _hub;

        public HomeController(IHub hub)
        {
            _hub = hub;
        }

        [Route("")]
        public IActionResult Index()
        {

            var childSpan = _hub.GetSpan()?.StartChild("additional-work");

            try
            {
                //var ss = 300;

                //byte asd = Convert.ToByte(ss);

                // Do the work that gets measured.

                childSpan?.Finish(SpanStatus.Ok);
            }
            catch (Exception e)
            {
                childSpan?.Finish(e);
                throw;
            }

            List<HaberContent>? mainSlider = new();
            mainSlider = (List<HaberContent>)connectDb.HaberContent.OrderByDescending(x => x.Id).Take(3).ToList();
            List<HaberContent>? FourNewsSliderRight = new();
            FourNewsSliderRight = (List<HaberContent>)connectDb.HaberContent.OrderByDescending(x => x.Id).Skip(3).Take(4).ToList();
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                MainSlider3 = mainSlider,
                FourNewsSliderRight = FourNewsSliderRight
            };

            string ipAddress = RockBreakerNugget.IpHelper.GetIpAddress();

            return View(homeIndexViewModel);
        }

        [Route("haberekle")]
        public IActionResult HaberEkle()
        {
            ReportingJob();
            return RedirectToAction("Index", "Home");
        }

        public static void ReportingJob()
        {
            //var parentid = BackgroundJob.Enqueue(() => HaberEkleJob());
            //Hangfire.RecurringJob.AddOrUpdate("HaberEkleJob", () => HaberEkleJob(), Cron.Minutely());
            //Hangfire.RecurringJob.AddOrUpdate("HaberEkleJob", () => HaberEkleJob(), "*/59 * * * *");
            //Hangfire.RecurringJob.AddOrUpdate("HaberEkleJob", () => HaberEkleJob(), Cron.MinuteInterval(59));
            Hangfire.RecurringJob.AddOrUpdate("HaberEkleJob", () => HaberEkleJob(), Cron.Hourly(1));
            //var parentid = Hangfire.BackgroundJob.Schedule(() => HaberEkleJob(), TimeSpan.FromMinutes(1));
            //Hangfire.BackgroundJob.ContinueJobWith(parentid, () => MailGonder(parentid));
        }

        public static void HaberEkleJob()
        {
            List<Result>? a = new();
            var options = new RestClientOptions("https://api.collectapi.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/news/getNews?country=tr&tag=general");
            request.AddHeader("Authorization", "apikey 4KjTBVuVynV8NShbhckJoF:5SxjWRMd3zbzuxIxqyvdjk");
            RestResponse response = client.Execute(request);
            if (response.Content != null)
            {
                Root? haberler = JsonConvert.DeserializeObject<Root>(response.Content);
                bool sonuc = haberler != null && haberler.success;
                a = sonuc ? haberler?.result : new List<Result>();
            }

            foreach (var item in a)
            {
                HaberContent haberContent = new HaberContent();
                haberContent.Title = item.name;
                haberContent.Description = item.description;
                haberContent.Url = item.url;
                haberContent.Image = item.image;
                haberContent.Source = item.source;
                haberContent.NewsDate = item.date;
                connectDb.HaberContent.Add(haberContent);
                connectDb.SaveChanges();
            }
            HaberGetirNewsApi();
            MailGonder();
        }

        public static void MailGonder()
        {
            SmtpClient client = new SmtpClient("webmail.marvelpilatesstudio.com", 587);
            MailMessage mesaj = new MailMessage();
            mesaj.To.Add($"erkan.salihoglu@hotmail.com");
            mesaj.From = new MailAddress("iletisim@marvelpilatesstudio.com");
            mesaj.Subject = "Haber Eklendi";
            mesaj.Body = $"Haberler Eklendi. Saat: {DateTime.Now.ToLongTimeString()} - Milisaniye: {DateTime.Now.Millisecond}";

            /* Spama Düşmeyi engelliyor */
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(System.Text.RegularExpressions.Regex.Replace(mesaj.Body, @"<(.|\n)*?>", string.Empty), null, "text/plain");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mesaj.Body, null, "text/html");
            mesaj.AlternateViews.Add(plainView);
            mesaj.AlternateViews.Add(htmlView);

            NetworkCredential guvenlik = new NetworkCredential("iletisim@marvelpilatesstudio.com", "6hK5npFnGuexDmB");
            client.Credentials = guvenlik;
            client.EnableSsl = true; // gmail olduğunda true yapıyoruz
            client.EnableSsl = false;
            client.Send(mesaj);
        }

        public static void HaberGetirNewsApi()
        {
            var options = new RestClientOptions("https://newsdata.io")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/1/news?apikey=pub_3613149184d607336c57f5b78f2d507aad1a9&language=tr", Method.Get);
            RestResponse response = client.Execute(request);
            Models.NewsDataIO.Root root = JsonConvert.DeserializeObject<Models.NewsDataIO.Root>(response.Content);
            List<Models.NewsDataIO.Result> list = root.results.ToList();
            foreach (var result in list)
            {
                HaberContent haberContent = new HaberContent();
                haberContent.Title = result.title;
                haberContent.Description = result.content;
                haberContent.Image = result.image_url;
                haberContent.Url = result.link;
                haberContent.NewsDate = Convert.ToDateTime(result.pubDate);
                haberContent.Source = result.source_id;
                connectDb.HaberContent.Add(haberContent);
                connectDb.SaveChanges();
            }
            MailGonder();
        }

    }
}