namespace WebServer.MVC.Attributes.Validation
{
    using System;
    using System.Text.RegularExpressions;

    public class EmailAttribute : ValidationAttribute
    {
        public EmailAttribute()
        {

        }
        public EmailAttribute(string message)
            :base(message)
        {

        }
        public override bool IsValid(object obj)
        {

            string value = (string)Convert.ChangeType(obj, typeof(string));

            return Regex.IsMatch(value, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
    }
}
