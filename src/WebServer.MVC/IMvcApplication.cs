namespace WebServer.MVC
{
    using System.Collections.Generic;
    using WebServer.HTTP;
    using WebServer.MVC.DependencyInjection;
    using WebServer.MVC.Middlewares.Builder;
    public interface IMvcApplication
    {
        void Configure(IApplicationBuilder app);

        void ConfigureServices(IServiceCollection services);
    }
}
