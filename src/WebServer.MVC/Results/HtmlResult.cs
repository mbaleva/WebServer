namespace WebServer.MVC.Results
{
    using System.Text;
    using WebServer.HTTP.Enums;
    public class HtmlResult : ActionResult
    {
        public HtmlResult(string content, HttpStatusCode responseStatusCode = HttpStatusCode.OK)
            : base(responseStatusCode)
        {
            this.Headers.AddHeader("Content-Type", "text/html; charset=utf-8");
            this.Body = Encoding.UTF8.GetBytes(content);
        }
    }
}
