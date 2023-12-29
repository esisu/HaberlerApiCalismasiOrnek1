using HaberlerApiCalismasiOrnek1.Areas.Admin.Models;
using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.Controllers
{
    //[AllowAnonymous]
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        private readonly UserManager<AppUser> _userManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //[Route("login")]
        public IActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLoginViewModel userSignInViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userSignInViewModel.Email, userSignInViewModel.Password, false, true);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Default", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Login", new { area = "Admin" });
                }
            }
            return View();
        }

        [Route("kayit-ol")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("kayit-ol")]
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel userRegisterViewModel)
        {
            AppUser appUser = new AppUser()
            {
                Name = userRegisterViewModel.Name,
                Surname = userRegisterViewModel.Surname,
                Email = userRegisterViewModel.Mail,
                UserName = userRegisterViewModel.Username,
                Gender = "Male",
                ImageUrl = "/erkansalihoglu.png"
            };
            if (userRegisterViewModel.Password == userRegisterViewModel.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, userRegisterViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }

        [Route("giris-yap")]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("giris-yap")]
        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginViewModel userLoginViewModel)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var hasUser = await _userManager.FindByEmailAsync(userLoginViewModel.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya Şifre Yanlış");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(hasUser, userLoginViewModel.Password, true, true);

            if (result.Succeeded)
            {
                TempData["Add"] = "True";
                return RedirectToAction("Index", "Default");
            }

            return View();
        }



    }
}
