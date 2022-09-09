namespace Template
{
    using System.Collections.Generic;
    using WebServer.HTTP;
    using WebServer.MVC;
    using WebServer.MVC.DependencyInjection;
    using WebServer.MVC.Identity;
    using WebServer.MVC.DependencyInjection.Extensions;
    using WebServer.MVC.Middlewares.Builder;

    public class StartUp : IMvcApplication
    {
        public void Configure(IApplicationBuilder app)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                    .AddSelfAsTransient<UserManager>();
        }
    }
}
