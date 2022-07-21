namespace WebServer.HTTP.Contracts
{
    using System;
    using System.Threading.Tasks;
    public interface IHttpServer
    {
        Task Start(int port);
    }
}
