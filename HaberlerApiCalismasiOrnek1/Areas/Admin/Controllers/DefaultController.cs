using HaberlerApiCalismasiOrnek1.Areas.Admin.Models;
using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sentry;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]

    public class DefaultController : Controller
    {
        private ConnectDb connectDb = new ConnectDb();

        private readonly SignInManager<AppUser> _signInManager;

        public DefaultController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [Route("adminanasayfa")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("cikis-yap")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Default");
        }
        [Route("yetki-kontrol")]
        public async Task<IActionResult> AccessDenied(string ReturnUrl)
        {
            ViewBag.message = "Erişmek istediğiniz sayfaya yetkiniz bulunmamaktadır.";
            return View();
        }
    }
}
