using System.ComponentModel.DataAnnotations;

namespace HaberlerApiCalismasiOrnek1.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Rol Adı alanını Lütfen Boş Bırakmayınız")]
        [Display(Name = "Rol Adı : ")]
        public string Name { get; set; }

    }
}
