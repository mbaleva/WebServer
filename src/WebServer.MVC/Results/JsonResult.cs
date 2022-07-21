namespace WebServer.MVC.Results
{
    using WebServer.HTTP.Enums;
    public class JsonResult : ActionResult
    {
        public JsonResult(byte[] body, HttpStatusCode code = HttpStatusCode.OK)
            : base(code)
        {
            this.Headers.AddHeader("Content-Type", "application/json");
            this.Body = body;
        }
    }
}
