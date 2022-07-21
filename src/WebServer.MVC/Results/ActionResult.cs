namespace WebServer.MVC.Results
{
    using WebServer.HTTP;
    using WebServer.HTTP.Enums;

    public abstract class ActionResult : HttpResponse, IActionResult
    {
        public ActionResult(HttpStatusCode code) : base(code)
        {

        }
        public ActionResult(byte[] body,
            string contentType, HttpStatusCode statusCode = HttpStatusCode.OK)
            :base(body, contentType, statusCode)
        {

        }
    }
}
