namespace WebServer.MVC.Attributes.Validation
{
    using System;
    using System.Text.RegularExpressions;
    public class RegexAttribute : ValidationAttribute
    {
        private string pattern;
        public RegexAttribute(string pattern, string message)
            :base(message)
        {
            this.pattern = pattern;
        }
        public override bool IsValid(object obj)
        {
            string value = (string)Convert.ChangeType(obj, typeof(string));

            return Regex.IsMatch(value, this.pattern);
        }
    }
}
