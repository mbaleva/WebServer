namespace WebServer.HTTP.Extensions.HTTP
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    public class CookieCollection : ICookieCollection
    {
        private Dictionary<string, Cookie> cookies
            = new Dictionary<string, Cookie>();
        public void AddCookie(string key, Cookie cookie)
        {
            if (this.cookies.ContainsKey(key))
            {
                this.cookies[key] = cookie;
                return;
            }
            this.cookies.Add(key, new Cookie { Name = key, Value = cookie.Value});
        }

        public bool ContainsCookie(string key)
        {
            return this.cookies.Any(x => x.Key == key);
        }

        public Cookie GetCookie(string key)
        {
            return this.cookies.Where(x => x.Key == key).FirstOrDefault().Value;
        }

        public bool HasCookies()
        {
            return this.cookies.Count != 0;
        }
        public IEnumerator<Cookie> GetEnumerator()
        {
            return this.cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
