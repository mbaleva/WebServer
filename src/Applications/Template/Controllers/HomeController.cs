namespace Template.Controllers
{
    using WebServer.MVC;
    using WebServer.MVC.Attributes.HTTP;
    using WebServer.MVC.Attributes.Security;
    using WebServer.MVC.Results;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return this.View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return this.View();
        }
    }
}
