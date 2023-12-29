using System.ComponentModel.DataAnnotations;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
        public string Password { get; set; }
    }
}
