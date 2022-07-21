namespace WebServer.HTTP
{
    using System;
    using WebServer.HTTP.Enums;
    using System.Collections.Generic;
    using System.Text;
    using WebServer.HTTP.Common;
    using WebServer.HTTP.Extensions.HTTP;

    public class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Headers = new HeaderCollection();
            this.Cookies = new CookieCollection();
        }

        public HttpResponse(byte[] body,
            string contentType, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            this.Body = body;
            this.StatusCode = statusCode;
            this.Cookies = new CookieCollection();
            this.Headers = new HeaderCollection();
            this.Headers.AddHeader("Content-Type", contentType);
            this.Headers.AddHeader("Content-Length", body.Length.ToString());
        }

        public byte[] Body { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public IHeaderCollection Headers { get; set; }

        public ICookieCollection Cookies { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}" + GlobalConstants.NewLine);

            foreach (var item in this.Headers)
            {
                sb.Append(item.ToString() + GlobalConstants.NewLine);
            }

            foreach (var item in this.Cookies)
            {
                sb.Append("Set-Cookie: " + item.ToString() + GlobalConstants.NewLine);
            }
            sb.Append(GlobalConstants.NewLine);
            return sb.ToString();
        }
    }
}
