namespace WebServer.MVC.Validation
{
    using System.Collections.Generic;
    using System.Collections.Immutable;

    public class ModelStateDictionary
    {
        private Dictionary<string, List<string>> errors;

        public ModelStateDictionary()
        {
            this.errors = new Dictionary<string, List<string>>();
            
        }

        public IReadOnlyDictionary<string, List<string>> Errors =>
            this.errors.ToImmutableDictionary();

        public bool IsValid => this.Errors.Count == 0;
        public void Add(string propertyName, string message)
        {
            if (!this.errors.ContainsKey(propertyName))
            {
                this.errors.Add(propertyName, new List<string>());
            }
            this.errors[propertyName].Add(message);
        }
    }
}
