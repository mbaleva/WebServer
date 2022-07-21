namespace Template.ViewModels.Users
{
    using WebServer.MVC.Attributes.Validation;
    public class LoginInputModel
    {
        [Required("Invalid username or password")]
        public string Username { get; set; }

        [Required("Invalid username or password")]
        public string Password { get; set; }
    }
}
