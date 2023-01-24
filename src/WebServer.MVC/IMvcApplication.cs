namespace WebServer.MVC
{
    using System.Collections.Generic;
    using WebServer.HTTP;
    using WebServer.MVC.DependencyInjection;
    using WebServer.MVC.Middlewares;

    public interface IMvcApplication
    {
        void Configure(List<Route> routeTable, IApplicationBuilder builder);

        void ConfigureServices(IServiceCollection services);
    }
}
