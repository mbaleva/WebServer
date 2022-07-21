# WebServer

This project contains of three main parts:
#### WebServer.HTTP - custom implementation of HTTP Server. It provides abstraction to work with HttpRequest, HttpResponse, Cookies, Header, etc.
#### WebServer.MVC - custom implementation of MVC framework. For now this framework supports only these functionalities: ViewEngine, Model Binding, Model Validation
#### Applications/ - in this folders should be put apps which will use the framework (for sample app see <a href="https://github.com/mbaleva/WebServer/tree/master/src/Applications/Template">Template</a>)

### The following contains description of each directory in the project:
#### <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.HTTP">WebServer.HTTP</a>
<ul>
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.HTTP">WebServer.HTTP</a>
     - It is the base directory for the HTTP part of the framework. Contains base model like: HttpRequest, HttpResponse, Route, Cookie, etc.
    It also contains logic for the HttpServer which is the entrypoint of every request.
  </li>
 
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.HTTP/Common">WebServer.HTTP.Common</a>
     - Contains only common constants for the Http part of the project
  </li>
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.HTTP/Contracts">WebServer.HTTP.Contracts</a>
     - Contains interfaces for HttpServer
  </li>
  
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.HTTP/Extensions">WebServer.HTTP.Extensions</a>
     - 
  </li>
</ul>

#### <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC">WebServer.MVC</a>
<ul>
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC">WebServer.MVC</a>
     - It is the base directory for the MVC part of the framework. It contains 3 of the core classes for the MVC part: Controller, HostBuilder, IMvcApplication
  </li>
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/ViewEngine">WebServer.MVC.ViewEngine</a>
     - Contains the view engine part of the framework - partial view support, view rendering, view parsing, etc.
  </li>
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/Validation">WebServer.MVC.Validation</a>
     - Contains only the ModelStateDictionary, which is responsible for the Model Validation
  </li>
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/Results">WebServer.MVC.Results</a>
     - Contains base ActionResult class and different types of action results like HtmlResult, ViewResult, JsonResult, NotFoundResult, etc.
  </li>
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/Results">WebServer.MVC.Results</a>
     - Contains base ActionResult class and different types of action results like HtmlResult, ViewResult, JsonResult, NotFoundResult, etc.
  </li>
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/Identity">WebServer.MVC.Identity</a>
     - Contains identity context, identity models and common identity services - for now only UserManager
  </li>
  
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/Exceptions">WebServer.MVC.Exceptions</a>
     - Contains custom exceptions
  </li>
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/DependencyInjection">WebServer.MVC.DependencyInjection</a>
     - Contains the part of the framework responsible for DI container, services creation, etc.
  </li>
  
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/ControllerHelpers">WebServer.MVC.ControllerHelpers</a>
     - Contains ControllerState class, which is needed in model validation part
  </li>
  
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/WebServer.MVC/Attributes">WebServer.MVC.Attributes</a>
     - Contains custom attributes responsible for Http methods, model validation and authorization
  </li>
</ul>
<h3>Samples</h3>
<ol>
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/Applications/Template">Template</a> - this template can be used as starting point for
    the framework. It contains users functionality (login, logout, register), home and privacy page.
  </li>
  
  <li>
    <a href="https://github.com/mbaleva/WebServer/tree/master/src/Applications/PCShop">PCShop</a> - it's a small PCShop, which runs completely on this framework
    and exposes framework functionality.
  </li>
</ol>
<h3> Getting started </h3>
<ol>
  <li>Create a standard .NET Core 3.1 console application</li>
  <li>Add project references to WebServer.HTTP and WebServer.MVC projects</li>
  <li>
  Create a Startup.cs file (can be different name) like this: 
  <br/>
  <pre>
  <code>
  public class Startup : IMvcApplication 
  {
    public void Configure(List<Route> routeTable)
    {
    }
    public void ConfigureServices(IServiceCollection services)
    {
    }
  }
  </code>
  </pre>
  </li>
  <li> In Program.Main create host like this:
    <pre>
      <code>
        public class Program
        {
            async static Task Main(string[] args)
            {
                // 5000 means port in which HTTP Server should be started
                await HostBuilder.CreateHostAsync(new Startup(), 5000);
            }
        }
      </code>
    </pre>
    HostBuilder is the entrypoint to the whole framework: it calls two startup methods (Configure and ConfigureServices), adds routes to the route table.
    It is also responsible for the model binding and part.
  </li>
  <li>Create folder Controllers</li>
  <li>Create HomeController class in folder Controllers like this: 
    <pre>
      <code>
        public class HomeController : Controller
        {
            [HttpGet("/")]
            public IActionResult Index()
            {
                return this.View();
            }
        }
      </code>
    </pre>
  </li>
  <li>Create Views folder</li>
  <li>In Views folder create Shared folder and create _Layout.cshtml file in it</li>
  <li>Create Home folder in Views folder and create Index.cshtml file in it</li>
  <li>Place some content in the Index.cshtml file. Example: 
    <pre>
        <code>
          <h1>Welcome to my first app</h1>
          @DateTime.Now.Year
        </code>
    </pre>
  </li>
  <li>Important: each file which is not .cs file needs to be copied to output direcory manually for now. This means that you should go through each file in
  Views and wwwroot folder, right click to the file > Properties and you should select `Copy to Output Directory` = `Always`
  </li>
  <li>Start the application and you should see on localhost:5000 response like this
  <img src="https://user-images.githubusercontent.com/78359718/179356281-83782004-bc76-40c5-b814-9286a38ee5f0.png">
  </li>
</ol>
<h2>For more detailed information of how this framework can be used, plase see README.md files in each directory</h3>
