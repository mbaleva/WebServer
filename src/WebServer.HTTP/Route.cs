namespace WebServer.HTTP
{
    using System;
    using WebServer.HTTP.Enums;
    
    public class Route
    {
        public Route(string path, HttpMethod method,
            Func<HttpContext, HttpResponse> action)
        {
            this.Path = path;
            this.Method = method;
            this.Action = action;
        }
        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public Func<HttpContext, HttpResponse> Action { get; set; }
    }
}
