namespace WebServer.MVC.Attributes.Security
{
    using System;
    public class NonAuthorizeAttribute : Attribute
    {
        public NonAuthorizeAttribute()
        {

        }

        public NonAuthorizeAttribute(string path = "/")
        {
            this.Path = path;
        }

        public string Path { get; set; }
        public bool NonAuthorize(string userId)
        {
            return userId != null;
        }
    }
}
