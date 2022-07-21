namespace PCShop.Controllers
{
    using PCShop.Data;
    using PCShop.Services.CPU;
    using PCShop.ViewModels.CPU;
    using System.Threading.Tasks;
    using WebServer.MVC;
    using WebServer.MVC.Attributes.HTTP;
    using WebServer.MVC.Results;

    public class CPUController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IProcessorService service;

        public CPUController(ApplicationDbContext dbContext, IProcessorService service)
        {
            this.dbContext = dbContext;
            this.service = service;
        }

        public IActionResult Create()
            => this.View();

        [HttpPost]
        public IActionResult Create(CreateInputModel model) 
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            this.service.Create(model).GetAwaiter().GetResult();
            return this.Redirect("/");
        }
        public IActionResult ById(string id) 
        {
            var model = this.service.GetById(id);
            return this.View(model);
        }
        public IActionResult All()
        {
            return this.View(this.service.GetAll());
        }
        public IActionResult Delete(string id) 
        {
            this.service.Delete(id);
            return this.Redirect("/");
        }
        public IActionResult Edit(string id) 
        {
            var model = this.service.GetById(id);
            return this.View(model);
        }
        [HttpPost]
        public IActionResult Edit(ByIdModel model) 
        {
            this.service.Update(model);
            return this.Redirect("/");
        }
    }
}
