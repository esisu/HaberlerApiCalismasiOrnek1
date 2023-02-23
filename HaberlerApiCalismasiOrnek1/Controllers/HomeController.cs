using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using RestSharp;
using RestRequest = RestSharp.RestRequest;
using HaberlerApiCalismasiOrnek1.DbConnectFolder;

namespace HaberlerApiCalismasiOrnek1.Controllers
{
    public class HomeController : Controller
    {
        ConnectDb connectDb = new ConnectDb();

        public IActionResult Index()
        {
            List<HaberContent>? a = new List<HaberContent>();

            a = connectDb.HaberContent.ToList();

            return View(a);
        }

        public IActionResult HaberEkle()
        {
            List<Result>? a = new List<Result>();
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
            return View(a);
        }


    }
}