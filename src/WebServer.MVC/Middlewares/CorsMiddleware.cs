using WebServer.HTTP;

namespace WebServer.MVC.Middlewares
{
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void Invoke(HttpContext context)
        {
        }
    }
}
