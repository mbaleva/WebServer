using WebServer.HTTP;

namespace WebServer.MVC.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public void Invoke(HttpContext context) 
        {
            _next.Invoke(context);
        }
    }
}
