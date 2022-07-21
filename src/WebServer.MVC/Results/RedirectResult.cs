namespace WebServer.MVC.Results
{
    using WebServer.HTTP.Enums;
    public class RedirectResult : ActionResult
    {
        public RedirectResult(string path, HttpStatusCode code = HttpStatusCode.FOUND)
            :base(code)
        {
            this.Headers.AddHeader("Location", path);
        }
    }
}
