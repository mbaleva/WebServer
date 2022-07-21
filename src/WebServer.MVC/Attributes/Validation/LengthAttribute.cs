using System;

namespace WebServer.MVC.Attributes.Validation
{
    public class LengthAttribute : ValidationAttribute
    {
        private int minValue;
        private int maxValue;

        public LengthAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public LengthAttribute(int minValue, int maxValue, string message)
            :base(message)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
        public override bool IsValid(object obj)
        {
            string value = (string)Convert.ChangeType(obj, typeof(string));

            return value.Length >= this.minValue && value.Length <= this.maxValue;
        }
    }
}
