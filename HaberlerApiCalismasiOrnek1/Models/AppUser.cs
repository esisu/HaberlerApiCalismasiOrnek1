using Microsoft.AspNetCore.Identity;

namespace HaberlerApiCalismasiOrnek1.Models
{
    public class AppUser : IdentityUser<int>
    {

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Gender { get; set; }

    }
}
