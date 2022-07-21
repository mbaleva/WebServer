namespace PCShop.Controllers
{
    using WebServer.MVC;
    using WebServer.MVC.Results;
    using PCShop.Services.Memory;
    using WebServer.MVC.Attributes.HTTP;
    using PCShop.Data.Models;

    public class MemoryController : Controller
    {
        private IMemoryService service;

        public MemoryController(IMemoryService service)
        {
            this.service = service;
        }
        public IActionResult Create()
            => this.View();
        [HttpPost]
        public IActionResult Create(RAM model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var id = this.service.Create(model);

            return this.Redirect($"/Memory/ById?id={id}");
        }
        public IActionResult ById(string id)
            => this.View(this.service.GetById(id));
        public IActionResult Delete(string id)
        {
            this.service.Delete(id);
            return this.Redirect("/Memory/All");
        }
        public IActionResult All()
            => this.View(this.service.All());

        public IActionResult Edit(string id)
            => this.View(this.service.GetById(id));

        [HttpPost]
        public IActionResult Edit(RAM model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            this.service.Update(model);
            return this.Redirect($"/Memory/All");
        }
    }
}
