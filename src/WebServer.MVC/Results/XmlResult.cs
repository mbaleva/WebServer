namespace WebServer.MVC.Results
{
    using WebServer.HTTP.Enums;
    public class XmlResult : ActionResult
    {
        public XmlResult(byte[] body, HttpStatusCode code = HttpStatusCode.OK)
            :base(code)
        {
            this.Headers.AddHeader("Content-Type", "application/xml");
            this.Body = body;
        }
    }
}
