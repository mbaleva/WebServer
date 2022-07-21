namespace WebServer.MVC.Results
{
    using WebServer.HTTP.Enums;
    public class TextResult : ActionResult
    {
        public TextResult(HttpStatusCode code = HttpStatusCode.OK)
            :base(code)
        {
            this.Headers.AddHeader("Content-Type", "application/text");
        }
    }
}
