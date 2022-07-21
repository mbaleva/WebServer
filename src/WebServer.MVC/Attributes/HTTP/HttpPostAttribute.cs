using WebServer.HTTP.Enums;

namespace WebServer.MVC.Attributes.HTTP
{
    public class HttpPostAttribute : HttpAttribute
    {
        public HttpPostAttribute()
        {

        }
        public HttpPostAttribute(string url)
        {
            this.Url = url;
        }

        public override HttpMethod Method => HttpMethod.POST;
    }
}
