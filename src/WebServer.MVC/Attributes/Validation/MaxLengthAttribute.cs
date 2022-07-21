namespace WebServer.MVC.Attributes.Validation
{
    using System;
    public class MaxLengthAttribute : ValidationAttribute
    {
        private int maxValue;

        public MaxLengthAttribute()
        {

        }
        public MaxLengthAttribute(int minValue, string message)
            : base(message)
        {
            this.maxValue = minValue;
        }
        public override bool IsValid(object obj)
        {
            int value = (int)Convert.ChangeType(obj, typeof(int));
            return value <= this.maxValue;
        }
    }
}
