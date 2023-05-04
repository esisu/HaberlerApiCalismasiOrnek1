using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestRequest = RestSharp.RestRequest;
using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using Hangfire;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace HaberlerApiCalismasiOrnek1.Controllers
{
    public class HomeController : Controller
    {
        static readonly ConnectDb connectDb = new();
        // adem aydemir
        public IActionResult Index()
        {
            List<HaberContent>? mainSlider = new();
            mainSlider = (List<HaberContent>)connectDb.HaberContent.OrderByDescending(x => x.Id).Take(3).ToList();

            List<HaberContent>? FourNewsSliderRight = new();
            FourNewsSliderRight = (List<HaberContent>)connectDb.HaberContent.OrderByDescending(x => x.Id).Skip(3).Take(4).ToList();

            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                MainSlider3 = mainSlider,
                FourNewsSliderRight = FourNewsSliderRight
            };

            return View(homeIndexViewModel);
        }

        public IActionResult HaberEkle()
        {
            //BackgroundJob.Enqueue(() => HaberEkleJob());

            ReportingJob();

            //List<Result>? a = new();
            //var options = new RestClientOptions("https://api.collectapi.com")
            //{
            //    MaxTimeout = -1,
            //};
            //var client = new RestClient(options);
            //var request = new RestRequest("/news/getNews?country=tr&tag=general");
            //request.AddHeader("Authorization", "apikey 4KjTBVuVynV8NShbhckJoF:5SxjWRMd3zbzuxIxqyvdjk");
            //RestResponse response = client.Execute(request);
            //if (response.Content != null)
            //{
            //    Root? haberler = JsonConvert.DeserializeObject<Root>(response.Content);
            //    bool sonuc = haberler != null && haberler.success;
            //    a = sonuc ? haberler?.result : new List<Result>();
            //}

            //foreach (var item in a)
            //{
            //    HaberContent haberContent = new HaberContent();
            //    haberContent.Title = item.name;
            //    haberContent.Description = item.description;
            //    haberContent.Url = item.url;
            //    haberContent.Image = item.image;
            //    haberContent.Source = item.source;
            //    haberContent.NewsDate = item.date;
            //    connectDb.HaberContent.Add(haberContent);
            //    connectDb.SaveChanges();
            //}
            return RedirectToAction("Index", "Home");
        }

        public static void ReportingJob()
        {
            //var parentid = BackgroundJob.Enqueue(() => HaberEkleJob());

            //Hangfire.RecurringJob.AddOrUpdate("HaberEkleJob", () => HaberEkleJob(), Cron.Minutely());
            Hangfire.RecurringJob.AddOrUpdate("HaberEkleJob", () => HaberEkleJob(), "*/5 * * * *");

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


            MailGonder();


        }


        public static void MailGonder()
        {
            SmtpClient client = new SmtpClient("webmail.marvelpilatesstudio.com", 587);
            MailMessage mesaj = new MailMessage();
            mesaj.To.Add($"erkan.salihoglu@hotmail.com");
            mesaj.From = new MailAddress("iletisim@marvelpilatesstudio.com");
            mesaj.Subject = "Haber Eklendi";
            mesaj.Body = $"Haberler Eklendi. Saat: {DateTime.Now.ToLongTimeString()}";

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

    }
}