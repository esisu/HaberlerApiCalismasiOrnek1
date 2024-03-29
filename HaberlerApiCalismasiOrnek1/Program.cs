
using HaberlerApiCalismasiOrnek1.DbConnectFolder;
using HaberlerApiCalismasiOrnek1.Models;
using Hangfire;
using Sentry;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ConnectDb>();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ConnectDb>();

//builder.Services.AddHangfire(config => config.UseSqlServerStorage("server=DESKTOP-E6SPDSH\\SQLEXPRESS;database=Haberler;integrated security=true;TrustServerCertificate=true;MultipleActiveResultSets=true;"));
builder.Services.AddHangfire(config => config.UseSqlServerStorage("Data Source=104.247.162.242\\MSSQLSERVER2019; Initial Catalog=erkansa1_habertrendol;User ID=erkansa1_admin;Password=@vz7Nx7vioP%9Kaj;Integrated Security=False;TrustServerCertificate=True;"));

builder.Services.AddHangfireServer();

builder.Services.AddSession(option =>
{
    //S�re 10 dk olarak belirlendi
    option.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "HaberlerAPIPanelCookie";
    options.LoginPath = new PathString("/giris-yap");
    options.LogoutPath = new PathString("/cikis-yap");
    options.AccessDeniedPath = new PathString("/yetki-kontrol");
    options.Cookie = cookieBuilder;
    options.ExpireTimeSpan = TimeSpan.FromDays(60);
    options.SlidingExpiration = true;
    //options.Cookie.HttpOnly = true;

    //ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    //options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});

builder.WebHost.UseSentry(configureOptions: options =>
{
    options.Dsn = "https://086b1bdf942fe832af6c71daf4906135@o4506654596005888.ingest.sentry.io/4506654602821632";
    options.Debug = true;
    options.TracesSampleRate = 1.0;
    options.ConfigureScope(action: scope =>
    {
        scope.Level = SentryLevel.Debug;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseHangfireDashboard("/hangfire");


app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new MyAuthorizationFilter() },
    //IsReadOnlyFunc = (DashboardContext context) => true
});

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "areas",
//        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});

// Enable automatic tracing integration.
// If running with .NET 5 or below, make sure to put this middleware
// right after "UseRouting()".
app.UseSentryTracing();

app.Run();
