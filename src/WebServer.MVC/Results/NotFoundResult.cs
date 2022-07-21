namespace WebServer.MVC.Results
{
    using WebServer.HTTP.Enums;
    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(HttpStatusCode code = HttpStatusCode.NotFound) 
            :base(code)
        {
        }
    }
}
