using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HaberlerApiCalismasiOrnek1.DbConnectFolder
{
    public class ConnectDb : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=DESKTOP-E6SPDSH\\SQLEXPRESS;database=Haberler;integrated security=true;TrustServerCertificate=true;MultipleActiveResultSets=true;");

        }

        public DbSet<HaberContent> HaberContent { get; set; }
    }
}
