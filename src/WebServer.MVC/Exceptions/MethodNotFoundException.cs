using System;

namespace WebServer.MVC.Exceptions
{
    public class MethodNotFoundException : Exception
    {
        public MethodNotFoundException(string message) 
            : base(message)
        {

        }
    }
}
