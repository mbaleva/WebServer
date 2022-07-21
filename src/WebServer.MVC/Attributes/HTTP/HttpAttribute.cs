namespace WebServer.MVC.Attributes.HTTP
{
    using System;
    using WebServer.HTTP.Enums;
    public abstract class HttpAttribute : Attribute
    {

        public string Url { get; set; }
        public abstract HttpMethod Method { get; }
    }
}
