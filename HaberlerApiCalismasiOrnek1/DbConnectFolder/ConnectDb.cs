using HaberlerApiCalismasiOrnek1.Models;
using Microsoft.EntityFrameworkCore;

namespace HaberlerApiCalismasiOrnek1.DbConnectFolder
{
    public class ConnectDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-E6SPDSH\\SQLEXPRESS;database=Haberler;integrated security=true;TrustServerCertificate=True;");
        }

        DbSet<HaberContent> HaberContents { get; set; }
    }
}
