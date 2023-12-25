namespace WebServer.MVC
{
    using System.Threading.Tasks;
    using WebServer.HTTP;
    using System.Diagnostics;
    using WebServer.HTTP.Contracts;
    using System.Collections.Generic;
    using WebServer.HTTP.Enums;
    using System.IO;
    using System.Reflection;
    using System.Linq;
    using System;
    using WebServer.MVC.DependencyInjection;
    using WebServer.MVC.Attributes.HTTP;
    using WebServer.MVC.Results;
    using WebServer.MVC.Attributes.Security;
    using WebServer.MVC.ControllerHelpers;
    using WebServer.MVC.Validation;
    using WebServer.MVC.Attributes.Validation;
    using WebServer.MVC.ViewEngine;
    using WebServer.MVC.Middlewares;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class HostBuilder
    {
        private static IControllerState controllerState = new ControllerState();
        public static async Task CreateHostAsync<TStartup>(int port)
        {
            List<Route> routeTable = new List<Route>();
            IServiceCollection services = new ServiceCollection();

            IMvcApplication application = Activator.CreateInstance(typeof(TStartup)) as IMvcApplication;

            application.ConfigureServices(services);
            application.Configure(routeTable, (IApplicationBuilder)services.GetRequiredService<IApplicationBuilder>());

            RegisterStaticFiles(routeTable);
            RegisterRoutes(routeTable, application, services);

            Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", $"http://localhost:{port}");
            IHttpServer server = new HttpServer(routeTable);
            await server.Start(port);
        }
        private static void RegisterStaticFiles(List<Route> routeTable)
        {
            string[] allStaticFiles = Directory.GetFiles("wwwroot", "*", SearchOption.AllDirectories);

            foreach (var file in allStaticFiles)
            {
                string url = file.Replace("wwwroot", string.Empty)
                    .Replace("\\", "/");

                routeTable.Add(CreateRoute(file, url));
            }
        }
        private static Route CreateRoute(string file, string url)
        {
            var fileContent = File.ReadAllBytes(file);
            var fileExtension = new FileInfo(file).Extension;

            string contentType;
            switch (fileExtension)
            {
                case ".txt":
                    contentType = "text/plain";
                    break;
                case ".js":
                    contentType = "text/javascript";
                    break;
                case ".html":
                    contentType = "text/html";
                    break;
                case ".css":
                    contentType = "text/css";
                    break;
                case ".jpg":
                    contentType = "image/jpg";
                    break;
                case ".jpeg":
                    contentType = "image/jpg";
                    break;
                case ".png":
                    contentType = "image/png";
                    break;
                case ".gif":
                    contentType = "image/gif";
                    break;
                case ".ico":
                    contentType = "image/vnd.microsoft.icon";
                    break;
                default:
                    contentType = "text/plain";
                    break;
            }
            return new Route(url, HttpMethod.GET, (request) =>
            {
                return new HttpResponse(fileContent, contentType, HttpStatusCode.OK);
            });
        }

        private static void RegisterRoutes(List<Route> routeTable,
            IMvcApplication application,
            IServiceCollection services)
        {
            var controllerTypes = application.GetType()
                .Assembly.GetTypes().Where(x => x.IsClass &&
                !x.IsAbstract && x.IsSubclassOf(typeof(Controller)));

            foreach (var controller in controllerTypes)
            {
                var methods = controller.GetMethods()
                    .Where(x => x.IsPublic && !x.IsStatic &&
                    x.DeclaringType == controller && !x.IsSpecialName);

                foreach (var method in methods)
                {
                    string url = "/" + controller.Name
                        .Replace("Controller", string.Empty) + "/" + method.Name;

                    HttpMethod httpMethod = HttpMethod.GET;

                    HttpAttribute attribute = (HttpAttribute)method.GetCustomAttributes(true)
                        .Where(x => x.GetType().IsSubclassOf(typeof(HttpAttribute)))
                        .FirstOrDefault();


                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (!string.IsNullOrEmpty(attribute?.Url))
                    {
                        url = attribute.Url;
                    }

                    routeTable.Add(new Route(url, httpMethod, context => Action(context, controller, method, services)));
                }
            }
        }
        private static HttpResponse Action(HttpContext context,
            Type controller, MethodInfo method,
            IServiceCollection services)
        {
            var builder = services.GetRequiredService<IApplicationBuilder>();
            Controller instance =
            services.CreateInstance(controller) as Controller;

            instance.HttpContext = context;
            var userId = instance.UserId();

            var attr = method.GetCustomAttributes().FirstOrDefault(x => x.GetType()
            == typeof(AuthorizeAttribute)) as AuthorizeAttribute;

            if (attr != null && !attr.IsLoggedIn(userId))
            {
                return instance.Redirect("/Users/Login");
            }
            if (attr != null && !attr.HasPermissionsToAccess(instance.User))
            {
                return new HttpResponse(HttpStatusCode.Forbidden);
            }

            var nonAuthorizeAttr = method.GetCustomAttributes()
                .FirstOrDefault(x => x.GetType() == typeof(NonAuthorizeAttribute)) as NonAuthorizeAttribute;

            if (nonAuthorizeAttr != null && nonAuthorizeAttr.NonAuthorize(userId))
            {
                return instance.Redirect(nonAuthorizeAttr.Path);
            }

            List<object> arguments = new List<object>();

            ParameterInfo[] parameters = method.GetParameters();

            foreach (var param in parameters)
            {
                var currentParam = GetParamFromRequest(context.Request, param.Name);
                var paramValue = Convert.ChangeType(currentParam, param.ParameterType);

                if (paramValue == null &&
                   param.ParameterType != typeof(string)
                   && param.ParameterType != typeof(int?))
                {
                    paramValue = Activator.CreateInstance(param.ParameterType);
                    var properties = param.ParameterType.GetProperties();
                    foreach (var property in properties)
                    {
                        var propertyHttpParamerValue = GetParamFromRequest(context.Request, property.Name);
                        var propertyParameterValue = Convert.ChangeType(propertyHttpParamerValue, property.PropertyType);
                        property.SetValue(paramValue, propertyParameterValue);
                    }
                }


                if (context.Request.HttpMethod == HttpMethod.POST)
                {
                    controllerState.Reset();
                    instance.ModelState = ValidateObject(paramValue);
                    controllerState.Initialize(instance);
                }



                arguments.Add(paramValue);
            }

            var response = method.Invoke(instance, arguments.ToArray()) as ActionResult;
            return response;
        }

        private static ModelStateDictionary ValidateObject(object paramValue)
        {
            var modelState = new ModelStateDictionary();

            var objectProperties = paramValue.GetType().GetProperties();

            foreach (var objectProperty in objectProperties)
            {
                var validationAttributes = objectProperty
                    .GetCustomAttributes()
                    .Where(type => type is ValidationAttribute)
                    .Cast<ValidationAttribute>()
                    .ToList();

                foreach (var validationAttribute in validationAttributes)
                {
                    if (validationAttribute.IsValid(objectProperty.GetValue(paramValue)))
                    {
                        continue;
                    }

                    modelState.Add(objectProperty.Name, validationAttribute.Message);
                }
            }

            return modelState;
        }

        private static string GetParamFromRequest(HttpRequest request, string name)
        {
            name = name.ToLower();
            if (request.FormData.Any(x => x.Key.ToLower() == name))
            {
                return request.FormData
                    .FirstOrDefault(x => x.Key.ToLower() == name).Value;
            }

            if (request.QueryData.Any(x => x.Key.ToLower() == name))
            {
                return request.QueryData
                    .FirstOrDefault(x => x.Key.ToLower() == name).Value;
            }
            return null;
        }
    }
}
