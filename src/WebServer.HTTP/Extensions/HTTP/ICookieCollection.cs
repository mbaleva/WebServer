namespace WebServer.HTTP.Extensions.HTTP
{
    using System.Collections.Generic;
    public interface ICookieCollection : IEnumerable<Cookie>
    {
        bool HasCookies();

        bool ContainsCookie(string key);

        void AddCookie(string key, Cookie cookie);

        Cookie GetCookie(string key);
    }
}
