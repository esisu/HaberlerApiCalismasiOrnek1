using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace HaberlerApiCalismasiOrnek1.Models
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return httpContext.User.Identity?.IsAuthenticated ?? false;
            return true;
        }
    }
}
