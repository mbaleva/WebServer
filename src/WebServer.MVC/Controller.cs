namespace WebServer.MVC
{
    using System.Runtime.CompilerServices;
    using WebServer.HTTP;
    using System.IO;
    using System.Text;
    using WebServer.MVC.ViewEngine;
    using WebServer.MVC.Results;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Xml.Serialization;
    using System.Linq;
    using WebServer.MVC.Validation;
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;
    using WebServer.MVC.Identity.EntityFrameworkCore;
    using WebServer.MVC.DependencyInjection;

    public abstract class Controller
    {
        internal IViewEngine ViewEngine;
        private IdentityDbContext dbContext;
        private const string UserIdSession = "WS_UserId";
        public HttpContext HttpContext { get; set; }
        public IdentityUser User { get; set; }

        public dynamic ViewBag { get; }

        public ModelStateDictionary ModelState { get; set; }
        public Controller()
        {
            this.ViewBag = new System.Dynamic.ExpandoObject();
            this.dbContext = new IdentityDbContext();
        }
        public ActionResult View(
            object viewModel = null,
            [CallerMemberName]string actionName = null)
        {
            string viewName = $"{this.GetType().Name.Replace("Controller", "")}_{actionName}";
            string body = this.ViewEngine.Html(
                viewModel, this.User, this.ModelState, this.ViewBag, viewName);

            string layout = this.ViewEngine.Html
                (viewModel, this.User, this.ModelState, this.ViewBag, "Layout");
            
            var responseHtml = layout.Replace("//RenderBody", body);

            HtmlResult response = new HtmlResult(responseHtml);
            return response;
        }
        public ActionResult Redirect(string path)
        {
            RedirectResult response = new RedirectResult(path);
            return response;
        }
        public ActionResult NotFound()
        {
            NotFoundResult result = new NotFoundResult();
            return result;
        }
        public void SignIn(string userId)
        {
            this.HttpContext.Request.Session[UserIdSession] = userId;
        }
        public void SignOut()
        {
            this.HttpContext.Request.Session[UserIdSession] = null;
        }

        public ActionResult JSON(object obj)
        {
            var jsonAsString = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var jsonBytes = Encoding.UTF8.GetBytes(jsonAsString);

            JsonResult result = new JsonResult(jsonBytes);
            return result;
        }

        public ActionResult XML(object obj)
        {

            var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(stringWriter, obj);
            var xmlAsString = stringWriter.ToString();

            XmlResult result = new XmlResult(Encoding.UTF8.GetBytes(xmlAsString));
            return result;
        }
        public bool IsSignedIn()
        {
            if (this.HttpContext.Request.Session.ContainsKey(UserIdSession) &&
                this.HttpContext.Request.Session[UserIdSession] != null)
            {
                return true;
            }
            return false;
        }
        public string UserId()
        {
            if (!this.HttpContext.Request.Session.Any(x => x.Key == UserIdSession))
            {
                return null;
            }
            var userId = this.HttpContext.Request.Session[UserIdSession];
            this.User = this.dbContext.Users.Where(x => x.Id == userId)
                .FirstOrDefault();
            return userId;
        }
        public ActionResult ErrorView(string message)
        {
            string layout = this.ViewEngine.Html(null, this.User, this.ModelState, this.ViewBag, "Layout");

            var body = $"<br><br>{message}<br><br>";
            var responseHtml = layout.Replace("//RenderBody", body);

            byte[] responseBody = Encoding.UTF8.GetBytes(responseHtml);
            HttpResponse response = new HttpResponse(responseBody, "text/html");
            return response as ActionResult;
        }
    }
}
