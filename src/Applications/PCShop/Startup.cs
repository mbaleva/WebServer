namespace PCShop
{
    using System.Collections.Generic;
    using WebServer.HTTP;
    using WebServer.MVC;
    using WebServer.MVC.DependencyInjection;
    using PCShop.Services.CPU;
    using PCShop.Services.GPU;
    using PCShop.Services.HardDrives;
    using PCShop.Services.Memory;
    using WebServer.MVC.Identity;
    using WebServer.MVC.DependencyInjection.Extensions;
    using WebServer.MVC.Middlewares;

    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable, IApplicationBuilder app)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddControllersWithViews()
                    .AddSelfAsTransient<UserManager>()
                    .AddTransient<IProcessorService, ProcessorService>()
                    .AddTransient<IGPUService, GPUService>()
                    .AddTransient<IHardDriveService, HardDriveService>()
                    .AddTransient<IMemoryService, MemoryService>();
        }
    }
}