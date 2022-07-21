namespace WebServer.HTTP
{
    using System.Text;
    public class Cookie
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public int MaxAge { get; set; }

        public bool HttpOnly { get; set; }

        public string Path { get; set; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.Name}={this.Value}; Path={this.Path};");
            if (MaxAge != 0)
            {
                sb.Append($" Max-Age={this.MaxAge};");
            }

            if (this.HttpOnly)
            {
                sb.Append(" HttpOnly;");
            }

            return sb.ToString();
        }
    }
}
