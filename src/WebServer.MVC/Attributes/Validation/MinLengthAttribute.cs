namespace WebServer.MVC.Attributes.Validation
{
    using System;
    public class MinLengthAttribute : ValidationAttribute
    {
        private int minValue;

        public MinLengthAttribute()
        {

        }
        public MinLengthAttribute(int minValue, string message)
            :base(message)
        {
            this.minValue = minValue;
        }
        public override bool IsValid(object obj)
        {
            int value = (int)Convert.ChangeType(obj, typeof(int));
            return value >= this.minValue;
        }
    }
}
