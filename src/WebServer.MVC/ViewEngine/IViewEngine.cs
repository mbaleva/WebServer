namespace WebServer.MVC.ViewEngine
{
    using WebServer.MVC.Validation;
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;
    public interface IViewEngine
    {
        string Html(object viewModel, IdentityUser user, 
            ModelStateDictionary ModelStateDict, dynamic viewBag, string viewName);
    }
}