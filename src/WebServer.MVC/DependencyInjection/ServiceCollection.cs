namespace WebServer.MVC.DependencyInjection
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using WebServer.MVC.Exceptions;
    using System.Reflection;
    using WebServer.MVC.ViewEngine;

    public class ServiceCollection : IServiceCollection
    {
        public ServiceCollection()
        {
            this.AddSelfInDIContainer();
        }
        private static List<ServiceDescriptor> dependencies
            = new List<ServiceDescriptor>();
        private void AddSelfInDIContainer() 
        {
            if (dependencies.Count > 0)
            {
                return;
            }
            dependencies.Add(new ServiceDescriptor
            {
                ServiceType = ServiceType.Singleton,
                ImplementationType = this.GetType(),
                InterfaceType = typeof(IServiceCollection)
            });
            dependencies.First().Instance = CreateInstance(this.GetType());
        }
        public IServiceCollection AddTransient<TInterface, TImplementation>() 
        {
            dependencies.Add(new ServiceDescriptor 
            {
                ImplementationType = typeof(TImplementation),
                InterfaceType = typeof(TInterface),
                ServiceType = ServiceType.Transient
            });
            return this;
        }
        public IServiceCollection AddSelfAsTransient<TType>() 
        {
            dependencies.Add(new ServiceDescriptor
            {
                ImplementationType = typeof(TType),
                InterfaceType = typeof(TType),
                ServiceType = ServiceType.Transient
            });
            return this;
        }
        public IServiceCollection AddSelfAsSingleton<TType>()
        {
            var serviceDescriptor = new ServiceDescriptor
            {
                ImplementationType = typeof(TType),
                InterfaceType = typeof(TType),
                ServiceType = ServiceType.Singleton
            };
            dependencies.Add(serviceDescriptor);
            serviceDescriptor.Instance = CreateInstance(typeof(TType));
            return this;
        }
        public IServiceCollection AddSelfAsTransient(Type type)
        {
            dependencies.Add(new ServiceDescriptor
            {
                ImplementationType = type,
                InterfaceType = type,
                ServiceType = ServiceType.Transient
            });
            return this;
        }
        public IServiceCollection AddSelfAsSingleton(Type type)
        {
            var serviceDescriptor = new ServiceDescriptor
            {
                ImplementationType = type,
                InterfaceType = type,
                ServiceType = ServiceType.Singleton
            };
            dependencies.Add(serviceDescriptor);
            serviceDescriptor.Instance = CreateInstance(type);
            return this;
        }
        public IServiceCollection AddSingleton<TInterface, TImplementation>() 
        {
            var serviceDescriptor = new ServiceDescriptor
            {
                ImplementationType = typeof(TImplementation),
                InterfaceType = typeof(TInterface),
                ServiceType = ServiceType.Singleton
            };
            dependencies.Add(serviceDescriptor);
            serviceDescriptor.Instance = CreateInstance(typeof(TImplementation));
            return this;
        }
        public TType GetRequiredService<TType>() 
        {
            var serviceDescriptor = dependencies
                .FirstOrDefault(x => x.InterfaceType == typeof(TType));

            if (serviceDescriptor == null)
            {
                throw new ServiceNotFoundException(typeof(TType).Name);
            }
            if (serviceDescriptor.Instance == null)
            {
                return (TType)CreateInstance(typeof(TType));
            }
            return (TType)serviceDescriptor.Instance;
        }
        public object CreateInstance(Type type) 
        {
            if (!dependencies.Any(x => x.ImplementationType == type))
            {
                throw new ServiceNotFoundException(type.Name);
            }
            if (dependencies.Any(x => x.ImplementationType == type && 
                        x.ServiceType == ServiceType.Singleton && x.Instance != null))
            {
                return dependencies.First(x => x.ImplementationType == type && 
                x.ServiceType == ServiceType.Singleton).Instance;
            }
            
            ConstructorInfo constructor = type.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Count())
                .FirstOrDefault();

            ParameterInfo[] ctorParams = constructor.GetParameters();

            List<object> parametersInstances = new List<object>();

            foreach (var param in ctorParams)
            {
                object currentParamValue = CreateInstance(param.ParameterType);
                parametersInstances.Add(currentParamValue);
            }
            var instance = constructor.Invoke(parametersInstances.ToArray());
            this.AttachViewEngineToConstructor(type, instance);
            return instance;
        }
        private object AttachViewEngineToConstructor(Type type, object instance)
        {
            if (!(type.IsSubclassOf(typeof(Controller))))
            {
                return instance;
            }
            var instanceAsController = (Controller)instance;
            instanceAsController.ViewEngine = this.GetRequiredService<IViewEngine>();
            return instance;
        }
    }
}
