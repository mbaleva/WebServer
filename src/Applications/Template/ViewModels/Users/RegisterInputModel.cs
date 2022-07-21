namespace Template.ViewModels.Users
{
    using WebServer.MVC.Attributes.Validation;
    public class RegisterInputModel
    {
        [Required("Username is required")]
        [Length(5, 50, "Username should be between 5 and 50 characters")]
        public string Username { get; set; }

        [Required("Email is required")]
        public string Email { get; set; }
        
        [Required("Invalid password")]
        public string Password { get; set; }

        [Required("Invalid password")]
        public string ConfirmPassword { get; set; }
    }
}
