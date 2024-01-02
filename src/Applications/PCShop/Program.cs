namespace PCShop
{
    using WebServer.MVC;
    using System.Threading.Tasks;
    public class Program
    {
        async static Task Main(string[] args)
        {
            await HostBuilder.CreateHostAsync<Startup>(5000);
        }
    }
}
