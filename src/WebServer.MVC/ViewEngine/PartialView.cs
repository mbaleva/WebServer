namespace WebServer.MVC.ViewEngine
{
    using System.IO;
    public class PartialView : IPartialView
    {
        public string RenderWidget()
        {
            string PartialViewsFolder = "Views/Shared/Partials/";
            string PartialViewsExt = ".cshtml";

            return File.ReadAllText($"{PartialViewsFolder}{this.GetType().Name}{PartialViewsExt}");
        }
    }
}
