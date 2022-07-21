namespace WebServer.MVC.DependencyInjection
{
    using System;
    public class ServiceDescriptor
    {
        public ServiceType ServiceType { get; set; }
        public Type InterfaceType { get; set; }
        public Type ImplementationType { get; set; }
        public object Instance { get; set; }
    }
}
