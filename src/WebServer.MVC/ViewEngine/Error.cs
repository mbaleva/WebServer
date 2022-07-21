namespace WebServer.MVC.ViewEngine
{
    using System.Collections.Generic;
    using System.Text;
    using WebServer.MVC.Validation;
    using WebServer.MVC.Identity.EntityFrameworkCore.Models;
    public class Error : IView
    {
        private IEnumerable<string> errors;
        private string code;
        public Error(IEnumerable<string> errors, string code)
        {
            this.errors = errors;
            this.code = code;
        }
        public string GenerateHtml(object viewModel, IdentityUser user, ModelStateDictionary ModelStateDict, dynamic viewBag)
        {
            var html = new StringBuilder();

            html.Append("<h1>Booooom!!! Compile errors in view!!! Fix your errors and then try" +
                "again, good luck :D</h1>");

            foreach (var item in this.errors)
            {
                html.AppendLine($"<li>{item}</li>");
            }
            html.AppendLine(code);
            return html.ToString();
        }
    }
}
