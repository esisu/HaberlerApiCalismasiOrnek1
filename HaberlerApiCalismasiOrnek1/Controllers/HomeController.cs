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
        public IActionResult Index()
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

            ConnectDb connectDb = new ConnectDb();

            HaberContent haberContent = new HaberContent();

            foreach (var item in a)
            {
                haberContent.Title = item.name;
                haberContent.Description = item.description;
                haberContent.Url = item.url;
                haberContent.Image = item.image;
                haberContent.Source = item.source;
                haberContent.date = item.date;
            }

            connectDb.Add(haberContent);
            connectDb.SaveChanges();

            return View(a);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}