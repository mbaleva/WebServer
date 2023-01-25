using WebServer.HTTP;

namespace WebServer.MVC.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void Invoke(HttpContext context)
        { 
            _next.Invoke(context);
        }
    }
}
