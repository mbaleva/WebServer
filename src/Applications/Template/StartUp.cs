namespace Template
{
    using System.Collections.Generic;
    using WebServer.HTTP;
    using WebServer.MVC;
    using WebServer.MVC.DependencyInjection;
    using WebServer.MVC.Identity;
    using WebServer.MVC.DependencyInjection.Extensions;

    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddSelfAsTransient<UserManager>();
        }
    }
}
