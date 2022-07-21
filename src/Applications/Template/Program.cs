namespace Template
{
    using WebServer.MVC;
    using System.Threading.Tasks;
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await HostBuilder.CreateHostAsync(new StartUp(), 80);
        }
    }
}
