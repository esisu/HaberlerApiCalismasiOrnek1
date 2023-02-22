using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using RestSharp;
using RestRequest = RestSharp.RestRequest;

namespace HaberlerApiCalismasiOrnek1.Controllers
{
    public class HomeController : Controller
    {
        public ILogger<HomeController> Logger1 { get; }

        public HomeController(ILogger<HomeController> logger)
        {
            Logger1 = logger;
        }

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

            return View(a);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}