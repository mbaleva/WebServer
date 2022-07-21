namespace WebServer.MVC.DependencyInjection
{
    using System;
    public interface IServiceCollection
    {
        IServiceCollection AddTransient<TInterface, TImplementation>();
        IServiceCollection AddSelfAsTransient<TType>();
        IServiceCollection AddSelfAsSingleton<TType>();
        IServiceCollection AddSelfAsTransient(Type type);
        IServiceCollection AddSelfAsSingleton(Type type);
        IServiceCollection AddSingleton<TInterface, TImplementation>();
        TType GetRequiredService<TType>();
        object CreateInstance(Type type);
    }
}
