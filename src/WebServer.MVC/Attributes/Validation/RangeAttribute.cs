namespace WebServer.MVC.Attributes.Validation
{
    using System;
    public class RangeAttribute : ValidationAttribute
    {
        private int min;
        private int max;

        public RangeAttribute(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public RangeAttribute(int min, int max, string message)
            : base(message)
        {
            this.max = max;
            this.min = min;
        }
        public override bool IsValid(object obj)
        {
            int value = (int)Convert.ChangeType(obj, typeof(int));

            return value >= this.min && value <= this.max;
        }
    }
}
