namespace PCShop.Controllers
{
    using PCShop.Services.CPU;
    using PCShop.Services.GPU;
    using PCShop.Services.HardDrives;
    using PCShop.Services.Memory;
    using PCShop.ViewModels.Laptops;
    using WebServer.MVC;
    using WebServer.MVC.Results;
    public class LaptopController : Controller
    {
        private IMemoryService memoryService;
        private IProcessorService processorService;
        private IGPUService gpuService;
        private IHardDriveService hddService;

        public LaptopController(IMemoryService memoryService, IProcessorService processorService, IGPUService gpuService, IHardDriveService hddService)
        {
            this.memoryService = memoryService;
            this.processorService = processorService;
            this.gpuService = gpuService;
            this.hddService = hddService;
        }

        public IActionResult Create() 
        {
            var viewModel = new CreateInputModel
            {
                AvaliableCPUs = this.processorService.GetAllAsKeyValuePairs(),
                AvaliableGPUs = this.gpuService.GetAllAsKeyValuePairs(),
                AvaliableHDDs = this.hddService.GetAllAsKeyValuePairs(),
                AvaliableRAMs = this.memoryService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }
        public IActionResult Create(CreateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            return this.Redirect("/");
        }
    }
}
