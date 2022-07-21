namespace PCShop.Controllers
{
    using WebServer.MVC;
    using PCShop.Services.GPU;
    using WebServer.MVC.Results;
    using PCShop.ViewModels.GPU;
    using WebServer.MVC.Attributes.HTTP;

    public class GPUController : Controller
    {
        private IGPUService service;

        public GPUController(IGPUService service)
        {
            this.service = service;
        }

        public IActionResult Create()
            => this.View();
        [HttpPost]
        public IActionResult Create(GPUViewModel model) 
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            var id = this.service.Create(model);
            return this.Redirect($"/GPU/ById?id={id}");
        }
        public IActionResult ById(string id)
            => this.View(this.service.GetById(id));
        public IActionResult Edit(string id)
            => this.View(this.service.GetById(id));
        [HttpPost]
        public IActionResult Edit(GPUViewModel model) 
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            this.service.Update(model);
            return this.Redirect($"/GPU/ById?id={model.Id}");
        }
        public IActionResult Delete(string id) 
        {
            this.service.Delete(id);
            return this.Redirect("/GPU/All");
        }
        public IActionResult All()
            => this.View(this.service.GetAll());
    }
}
