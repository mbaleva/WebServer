namespace WebServer.MVC.ViewEngine
{
    using System;
    internal class ViewDescriptor
    {
        public string CSharpCode { get; set; }
        public string ViewName { get; set; }
        public bool IsSpecial { get; set; }

        public IView Instance { get; set; }
        public string ViewModelName { get; set; }
        public Type ViewModelType { get; set; }
    }
}
