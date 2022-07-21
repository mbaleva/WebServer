namespace WebServer.HTTP.Extensions.HTTP
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Collections;

    public class HeaderCollection : IHeaderCollection
    {
        private Dictionary<string, Header> headers =
            new Dictionary<string, Header>();
        public void AddHeader(string key, string value)
        {
            this.headers.Add(key, new Header { Name = key, Value = value});
        }

        public bool Contains(string key)
        {
            return this.headers.ContainsKey(key);
        }

        public Header GetHeader(string name)
        {
            var header = this.headers.Where(x => x.Key == name).FirstOrDefault();
            return header.Value;
        }

        public bool HasHeaders()
        {
            return this.headers.Count != 0;
        }
        public IEnumerator<Header> GetEnumerator()
        {
            return this.headers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
