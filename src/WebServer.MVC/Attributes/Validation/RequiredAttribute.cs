namespace WebServer.MVC.Attributes.Validation
{
    using System;
    public class RequiredAttribute : ValidationAttribute
    {
        public RequiredAttribute(string message) :
            base(message)
        {
        }
        public RequiredAttribute()
        {
        }
        public override bool IsValid(object obj)
        {
            return obj != null;
        }
    }
}
