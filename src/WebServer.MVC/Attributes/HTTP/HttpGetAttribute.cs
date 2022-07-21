using WebServer.HTTP.Enums;

namespace WebServer.MVC.Attributes.HTTP
{
    public class HttpGetAttribute : HttpAttribute
    {
        public HttpGetAttribute()
        {

        }
        public HttpGetAttribute(string url)
        {
            this.Url = url;
        }
        public override HttpMethod Method { get => HttpMethod.GET; }
    }
}
