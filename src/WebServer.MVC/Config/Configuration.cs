namespace WebServer.MVC.Config
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;

    public class Configuration : IConfiguration
    {
        private readonly string jsonAsText;
        private JObject parsedObject;
        public Configuration()
        {
            if (!File.Exists("appsettings.json"))
            {
                throw new FileNotFoundException("File appsettings.json is not presented in build directory. Please make sure you edited the properties of the file to Copy always to output directory");
            }
            this.jsonAsText = File.ReadAllText("appsettings.json");
            this.parsedObject = JObject.Parse(this.jsonAsText);
        }
        public T GetValue<T>(string name)
            =>(T)Convert.ChangeType(parsedObject[name].ToString(), typeof(T));

    }
}
