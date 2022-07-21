namespace WebServer.MVC.Attributes.Validation
{
    using System;
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidationAttribute : Attribute
    {
        public ValidationAttribute(string message = "Error")
        {
            this.Message = message;
        }
        public string Message { get; set; }

        public abstract bool IsValid(object obj);
    }
}
