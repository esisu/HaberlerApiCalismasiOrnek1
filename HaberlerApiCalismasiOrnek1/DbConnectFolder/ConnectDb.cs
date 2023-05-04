using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.EntityFrameworkCore;

namespace HaberlerApiCalismasiOrnek1.DbConnectFolder
{
    public class ConnectDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-E6SPDSH\\SQLEXPRESS;database=Haberler;integrated security=true;TrustServerCertificate=true;MultipleActiveResultSets=true;");
            //optionsBuilder.UseSqlServer("server=ISU-NB-00414\\SQLEXPRESS;database=Haberler;integrated security=true;TrustServerCertificate=true;MultipleActiveResultSets=true;");
        }

        public DbSet<HaberContent> HaberContent { get; set; }
    }
}
