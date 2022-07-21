namespace Template.Controllers
{
    using System;
    using Template.ViewModels.Users;
    using WebServer.MVC;
    using WebServer.MVC.Attributes.HTTP;
    using WebServer.MVC.Identity;
    using WebServer.MVC.Results;

    public class UsersController : Controller
    {
        private UserManager userManager;
        public UsersController(UserManager userManager) 
        {
            this.userManager = userManager;
        }
        public IActionResult Login() 
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Login(LoginInputModel model) 
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            var userId = this.userManager.Login(model.Username, model.Password);
            if (userId == null)
            {
                return this.ErrorView("Invalid credentials");
            }
            this.SignIn(userId);
            return this.Redirect("/");
        }
        public IActionResult Register() 
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult Register(RegisterInputModel model) 
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            this.userManager.Register(model.Username, model.Email, model.Password);
            return this.Redirect("/");
        }
        public IActionResult Logout() 
        {
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
