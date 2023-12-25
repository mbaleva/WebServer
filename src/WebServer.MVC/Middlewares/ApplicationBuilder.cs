using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.ExceptionServices;
using WebServer.HTTP;
using WebServer.MVC.DependencyInjection;
using WebServer.MVC.Exceptions;

namespace WebServer.MVC.Middlewares
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly LinkedList<Type> middlewaresChain;
        public ApplicationBuilder()
        {
            this.middlewaresChain = new LinkedList<Type>();
        }
        public void UseMiddleware<TMiddleware>()
            where TMiddleware : class
        {
            if (this.middlewaresChain.Count == 0)
            {
                this.middlewaresChain.AddFirst(typeof(TMiddleware));
                return;
            }
            this.middlewaresChain.AddLast(typeof(TMiddleware));
        }
        public void TriggerRequestPipeline(IServiceCollection services, HttpContext context) 
        {
            var firstMiddleware = this.middlewaresChain.First;
             
            var second = firstMiddleware.Next.Value.GetMethod("Invoke").CreateDelegate(typeof(RequestDelegate), null);
            var current = Activator.CreateInstance(firstMiddleware.Value, second);
            dynamic castedMiddleware = Convert.ChangeType(current, firstMiddleware.Value);
            castedMiddleware.Invoke(context);
        }
    }
}
