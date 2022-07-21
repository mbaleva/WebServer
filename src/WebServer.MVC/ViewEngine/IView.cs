namespace WebServer.MVC.ViewEngine
{
    using WebServer.MVC.Validation;
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;
    public interface IView
    {
        string GenerateHtml(object viewModel, IdentityUser user, 
            ModelStateDictionary ModelStateDict, dynamic viewBag);
    }
}
