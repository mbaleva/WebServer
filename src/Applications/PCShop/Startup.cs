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
    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddAsSelf<UserManager>()
                    .Add<IProcessorService, ProcessorService>()
                    .Add<IGPUService, GPUService>()
                    .Add<IHardDriveService, HardDriveService>()
                    .Add<IMemoryService, MemoryService>();
        }
    }
}