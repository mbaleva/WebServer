namespace WebServer.HTTP.Extensions.HTTP
{
    using System.Collections.Generic;
    public interface IHeaderCollection : IEnumerable<Header>
    {
        void AddHeader(string key, string value);

        bool Contains(string key);

        bool HasHeaders();

        Header GetHeader(string name);
    }
}
