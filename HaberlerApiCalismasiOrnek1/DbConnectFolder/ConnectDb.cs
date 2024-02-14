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
            optionsBuilder.UseSqlServer("Data Source=104.247.162.242\\MSSQLSERVER2019; Initial Catalog=erkansa1_habertrendol;User ID=erkansa1_admin;Password=@vz7Nx7vioP%9Kaj;Integrated Security=False;TrustServerCertificate=True;");
        }

        public DbSet<HaberContent> HaberContent { get; set; }
    }
}
