namespace WebServer.HTTP
{
    using WebServer.HTTP.Contracts;
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Linq;
    using WebServer.HTTP.Enums;

    public class HttpServer : IHttpServer
    {
        private List<Route> routes;

        public HttpServer(List<Route> routes)
        {
            this.routes = routes;
        }
        public async Task Start(int port)
        {
            TcpListener tcpListener =
                new TcpListener(IPAddress.Loopback, port);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                ProcessTcpClientAsync(tcpClient);
            }
        }
        private async Task ProcessTcpClientAsync(TcpClient client)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (NetworkStream stream = client.GetStream())
                {
                    int positionToRead = 0;

                    while (true)
                    {
                        byte[] buffer = new byte[2048];
                        int count = stream.Read(buffer, positionToRead, buffer.Length);
                        positionToRead += count;
                        string text = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        sb.Append(text);

                        if (count < buffer.Length)
                        {
                            break;
                        }
                    }
                    HttpContext context = new HttpContext();
                    context.Request = new HttpRequest(sb.ToString());

                    var route = this.routes.FirstOrDefault(
                               x => string.Compare(x.Path, context.Request.Path, true) == 0
                                   && x.Method == context.Request.HttpMethod);


                    if (route != null)
                    {
                        context.Response = route.Action(context);
                    }
                    else
                    {
                        context.Response = new HttpResponse(new byte[0], "text/html", Enums.HttpStatusCode.NotFound);
                    }

                    var sessionCookie = context.Request.Cookies
                        .FirstOrDefault(x => x.Name == "WS_SID");

                    if (sessionCookie != null)
                    {
                        Cookie responseCookie = new Cookie();
                        responseCookie.Name = sessionCookie.Name;
                        responseCookie.Value = sessionCookie.Value;

                        responseCookie.Path = "/";

                        if (!context.Response.Cookies.ContainsCookie("WS_SID"))
                        {
                            context.Response.Cookies.AddCookie(responseCookie.Name, responseCookie);
                        }
                    }

                    var responseHeaderBytes = Encoding.UTF8.GetBytes(context.Response.ToString());
                    await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);

                    if (context.Response.Body != null)
                    {
                        await stream.WriteAsync(context.Response.Body, 0, context.Response.Body.Length);
                    }
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
