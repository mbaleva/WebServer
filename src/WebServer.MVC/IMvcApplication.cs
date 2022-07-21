namespace WebServer.MVC
{
    using System.Collections.Generic;
    using WebServer.HTTP;
    using WebServer.MVC.DependencyInjection;
    public interface IMvcApplication
    {
        void Configure(List<Route> routeTable);

        void ConfigureServices(IServiceCollection services);
    }
}
