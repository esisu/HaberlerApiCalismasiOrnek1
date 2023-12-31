using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]

    public class DefaultController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public DefaultController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [Route("adminanasayfa")]
        public async Task<IActionResult> Index()
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
