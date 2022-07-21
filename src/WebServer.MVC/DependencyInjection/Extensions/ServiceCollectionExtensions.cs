namespace WebServer.MVC.DependencyInjection.Extensions
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using System;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddControllers(this IServiceCollection services)
        {
            List<Type> controllers = Assembly
                .GetEntryAssembly()?
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)))
                .ToList();

            foreach (var controller in controllers) 
            {
                services.AddSelfAsTransient(controller);
            }
            return services;
        }
        public static IServiceCollection AddControllersWithViews(this IServiceCollection services)
        {
            List<Type> controllers = Assembly
                .GetEntryAssembly()?
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)))
                .ToList();

            foreach (var controller in controllers)
            {
                services.AddSelfAsTransient(controller);
            }
            services.AddSingleton<ViewEngine.IViewEngine, ViewEngine.ViewEngine>();
            return services;
        }
    }
}
