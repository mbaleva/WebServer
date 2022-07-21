namespace PCShop.Controllers
{
    using WebServer.MVC;
    using WebServer.MVC.Attributes.HTTP;
    using WebServer.MVC.Results;

    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index() 
        {
            return this.View();
        }
        public IActionResult Privacy() 
        {
            return View(); 
        }
    }
}
