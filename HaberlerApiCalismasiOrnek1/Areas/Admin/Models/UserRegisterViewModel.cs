using System.ComponentModel.DataAnnotations;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.Models
{
    public class UserRegisterViewModel
    {

        [Required(ErrorMessage = "Adınızı Giriniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyadınızı Giriniz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adınızı Giriniz")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mail Adresinizi Giriniz")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifrenizi Giriniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Tekrar Şifre Giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler uyumlu değil")]
        public string ConfirmPassword { get; set; }

    }
}
