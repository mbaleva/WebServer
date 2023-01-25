using WebServer.HTTP;

namespace WebServer.MVC.Middlewares
{
    public delegate void RequestDelegate(HttpContext context);
}
