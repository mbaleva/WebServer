namespace WebServer.HTTP
{
    using System;
    using System.Collections.Generic;
    using WebServer.HTTP.Enums;
    using System.Linq;
    using System.Text;
    using System.Net;
    using WebServer.HTTP.Extensions.HTTP;
    using WebServer.HTTP.Common;

    public class HttpRequest
    {
        public static IDictionary<string, Dictionary<string, string>> Sessions =
            new Dictionary<string, Dictionary<string, string>>();

        public Dictionary<string, string> Session = new Dictionary<string, string>();
        public Dictionary<string, string> FormData { get; set; }

        public string QueryString { get; set; }

        public Dictionary<string, string> QueryData { get; set; }
        public IHeaderCollection Headers { get; set; }
        public HttpMethod HttpMethod { get; set; }

        public ICookieCollection Cookies { get; set; }

        public string Path { get; set; }

        public string Body { get; set; }
        public HttpRequest(string request)
        {
            this.Headers = new HeaderCollection();
            this.Cookies = new Extensions.HTTP.CookieCollection();
            this.FormData = new Dictionary<string, string>();
            this.QueryData = new Dictionary<string, string>();
            this.ExtractData(request);
        }
        private void ExtractData(string request)
        {
            string[] requestSplittedByRows = request.Split(new string[] { GlobalConstants.NewLine }, StringSplitOptions.None);
            //requestSplittedByRows[requestSplittedByRows.Length - 1] = null;
            bool isInBody = false;
            StringBuilder sb = new StringBuilder();


            for (int i = 0; i < requestSplittedByRows.Length; i++)
            {
                try
                {
                    if (i == 0)
                    {
                        if (requestSplittedByRows[0] == null)
                        {
                            continue;
                        }
                        string[] firstLine = requestSplittedByRows[0].Split(' ');
                        string httpMethodAsString = firstLine[0];
                        this.HttpMethod = (HttpMethod)Enum.Parse(typeof(HttpMethod), httpMethodAsString);
                        this.Path = firstLine[1];

                        if (httpMethodAsString == "GET")
                        {
                            requestSplittedByRows[requestSplittedByRows.Length - 1] = null;
                        }
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(requestSplittedByRows[i]))
                    {
                        isInBody = true;
                        continue;
                    }

                    if (isInBody)
                    {
                        sb.Append(requestSplittedByRows[i]);
                    }
                    else
                    {
                        this.AddHeader(requestSplittedByRows[i]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (this.Headers.Contains("Cookie"))
            {
                var cookiesAsString = this.Headers.GetHeader("Cookie").Value;
                var cookies = cookiesAsString.Split(new string[] { "; " },
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookieAsString in cookies)
                {
                    this.AddCookie(cookieAsString);
                }
            }

            Cookie sessionCookie = this.Cookies.GetCookie("WS_SID");

            if (sessionCookie == null)
            {
                string sessionId = Guid.NewGuid().ToString();

                this.Session = new Dictionary<string, string>();

                Sessions.Add(sessionId, this.Session);
                this.AddCookie("WS_SID", sessionId);
            }
            else if (!Sessions.ContainsKey(sessionCookie.Value))
            {
                this.Session = new Dictionary<string, string>();
                Sessions.Add(sessionCookie.Value, this.Session);
            }
            else
            {
                this.Session = Sessions[sessionCookie.Value];
            }

            this.Body = sb.ToString().TrimEnd('\n', '\r');

            if (this.Body != null)
            {
                this.SplitParams(this.Body, this.FormData);
            }

            if (this.Path.Contains('?'))
            {
                string[] parts = this.Path.Split(new char[] { '?' }, 2);
                this.Path = parts[0];
                this.QueryString = parts[1];
                this.SplitParams(this.QueryString, this.QueryData);
            }
            else
            {
                this.QueryString = string.Empty;
            }
        }
        private void AddCookie(string name, string value)
        {
            Cookie cookie = new Cookie();
            cookie.Name = name;
            cookie.Value = value;
            this.Cookies.AddCookie(name, new Cookie { Name = name, Value = value});
        }
        private void AddCookie(string cookieAsString)
        {
            string[] cookieParts = cookieAsString.Split(new string[] { "=" }, 2, StringSplitOptions.None);
            Cookie cookie = new Cookie();
            cookie.Name = cookieParts[0];
            cookie.Value = cookieParts[1];
            this.Cookies.AddCookie(cookie.Name, cookie);
        }
        private void AddHeader(string line)
        {
            Header header = new Header();
            string[] headerParts = line.Split(new string[] { ": " }, 2, StringSplitOptions.None);
            header.Name = headerParts[0];
            header.Value = headerParts[1];
            this.Headers.AddHeader(header.Name, header.Value);
        }
        private void SplitParams(string param, Dictionary<string, string> output)
        {
            string[] parts = param.Split('&');

            foreach (var item in parts)
            {
                if (item == null || item == "\r\n" || item == string.Empty)
                {
                    continue;
                }
                string[] current = item.Split('=');
                string name = current[0];
                string value = WebUtility.UrlDecode(current[1]);

                if (!output.ContainsKey(name))
                {
                    output.Add(name, value);
                }
            }
        }
    }
}
