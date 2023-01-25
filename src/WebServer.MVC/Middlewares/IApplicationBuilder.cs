using System;
using System.Collections.Generic;
using System.Text;
using WebServer.HTTP;
using WebServer.MVC.DependencyInjection;

namespace WebServer.MVC.Middlewares
{
    public interface IApplicationBuilder
    {
        void UseMiddleware<TMiddleware>() where TMiddleware : class;
        void TriggerRequestPipeline(IServiceCollection services, HttpContext context);
    }
}
