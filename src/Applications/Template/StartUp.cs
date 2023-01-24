namespace Template
{
    using System.Collections.Generic;
    using WebServer.HTTP;
    using WebServer.MVC;
    using WebServer.MVC.DependencyInjection;
    using WebServer.MVC.Identity;
    using WebServer.MVC.DependencyInjection.Extensions;
    using WebServer.MVC.Middlewares;

    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable, IApplicationBuilder builder)
        {
            builder.UseMiddleware<AuthenticationMiddleware>();
            builder.UseMiddleware<AuthorizationMiddleware>();
            builder.UseMiddleware<CorsMiddleware>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddSelfAsTransient<UserManager>();
            services.AddSingleton<IApplicationBuilder, ApplicationBuilder>();
        }
    }
}
