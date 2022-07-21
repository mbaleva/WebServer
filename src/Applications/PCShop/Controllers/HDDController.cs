namespace PCShop.Controllers
{
    using WebServer.MVC;
    using WebServer.MVC.Results;
    using PCShop.Services.HardDrives;
    using PCShop.Data.Models;
    using WebServer.MVC.Attributes.HTTP;

    public class HDDController : Controller
    {
        private IHardDriveService service;

        public HDDController(IHardDriveService service)
        {
            this.service = service;
        }
        public IActionResult Create()
            => this.View();
        [HttpPost]
        public IActionResult Create(HardDrive hdd) 
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(hdd);
            }
            var id = this.service.Create(hdd);
            return this.Redirect($"/HDD/ById?id={id}");
        }
        public IActionResult ById(string id)
            => this.View(this.service.GetById(id));
        public IActionResult Delete(string id)
        {
            this.service.Delete(id);
            return this.Redirect("/HDD/All");
        }
        public IActionResult All()
         => this.View(this.service.GetAll());
        public IActionResult Edit(string id)
        {
            return this.View(this.service.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(HardDrive hdd)
        {
            this.service.Update(hdd);
            return this.Redirect($"/HDD/ById?id={hdd.Id}");
        }
    }
}
