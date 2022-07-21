namespace WebServer.MVC.Exceptions
{
    using System;
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException()
        {
        }
        public ServiceNotFoundException(string type)
            :base($"An error occured while trying to create instance of {type}." +
                 $"Service not found in dependency injection container. Please make sure" +
                 $"you added all of the necessary types into DI container at Startup.ConfigureServices method")
        {

        }
    }
}
