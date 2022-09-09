namespace WebServer.MVC.Config
{
    public interface IConfiguration
    {
        T GetValue<T>(string name);
    }
}
